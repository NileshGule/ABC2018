using System;
using RabbitMQ.Client;
using System.Threading;
using TechTalksELKProcessor.Documents;
using TechTalksELKProcessor.Indexer;
using RabbitMQ.Client.Events;
using System.Text;
using Newtonsoft.Json;
using TechTalksModel.DTO;

namespace TechTalksELKProcessor
{
    class Program
    {
        private static ManualResetEvent _ResetEvent = new ManualResetEvent(false);

        private const string exchangeName = "TechTalksExchange";
        // private const string queueName = "hello";
        private const string routingKey = "hello";

        private const string QUEUE_GROUP = "index-handler";

        static void Main(string[] args)
        {
            Console.WriteLine("Initializing Elasticsearch. url: {0}", "http://elk:9200/");
            Index.Setup();

            var factory = new ConnectionFactory() { HostName = "rabbitmq"};
        
            using (var connection = factory.CreateConnection())
            {
                Console.WriteLine("Inside connection");

                using (var channel = connection.CreateModel())
                {
                    Console.WriteLine("Inside model");
                    channel.ExchangeDeclare(exchangeName, "fanout");
                    
                    string queueName = channel.QueueDeclare().QueueName;
                    
                    channel.QueueBind(queueName, exchangeName, routingKey);

                    var consumer = new EventingBasicConsumer(channel);
                  
                    consumer.Received += RabbitMQBasicMessageHandler;

                    channel.BasicConsume(queue: queueName,
                                        autoAck: true,
                                        consumer: consumer);
                    
                    Console.WriteLine($"Listening to events on {queueName}");
                    _ResetEvent.WaitOne();
                }
            }            
        }

        private static void RabbitMQBasicMessageHandler(object model, BasicDeliverEventArgs ea)
        {
            Console.WriteLine("Inside ELK receiver...");
            var body = ea.Body;
            var message = Encoding.UTF8.GetString(body);
            var techTalkModel = JsonConvert.DeserializeObject<TechTalksModel.TechTalk>(message);

            TechTalk techTalk = new TechTalk
            {
                TechTalkName = techTalkModel.TechTalkName,
                Category = techTalkModel.Category.CategoryName,
                Level = techTalkModel.Level.LevelName,
                EventTime = DateTime.UtcNow
            };
            

            Console.WriteLine($"Received message {message}");

            Console.WriteLine($"Tech Talk Name : {techTalk.TechTalkName}");
            Console.WriteLine($"Category : {techTalk.Category}");
            
            Index.CreateDocument(techTalk);
            Console.WriteLine("Index written successfully");
        }
    }
}
