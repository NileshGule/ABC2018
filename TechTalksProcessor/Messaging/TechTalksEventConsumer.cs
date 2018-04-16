using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Newtonsoft.Json;
using TechTalksModel;
using System.Threading;

namespace TechTalksProcessor.Messaging
{
    public class TechTalksEventConsumer : ITechTalksEventConsumer
    {
        private const string exchangeName = "TechTalksExchange";
        // private const string queueName = "hello";
        private const string routingKey = "hello";

        private static ManualResetEvent _ResetEvent = new ManualResetEvent(false);

        private readonly TechTalksDBContext _context;
        public TechTalksEventConsumer(TechTalksDBContext context)
        {
            _context = context;
        }

        public void ConsumeMessage()
        {
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
                  
                    consumer.Received += RabbitMQEventHandler;

                    channel.BasicConsume(queue: queueName,
                                        autoAck: true,
                                        consumer: consumer);

                    Console.WriteLine($"Listening to events on {queueName}");
                    
                    _ResetEvent.WaitOne();
                }
            }
        }

        private void RabbitMQEventHandler(object model, BasicDeliverEventArgs ea)
        {
            Console.WriteLine("Inside RabbitMQ receiver...");
            var body = ea.Body;
            var message = Encoding.UTF8.GetString(body);
            var techTalk = JsonConvert.DeserializeObject<TechTalk>(message);
            Console.WriteLine($"Received message {message}");

            Console.WriteLine($"Tech Talk Id : {techTalk.Id}");
            Console.WriteLine($"Tech Talk Name : {techTalk.TechTalkName}");
            Console.WriteLine($"Category : {techTalk.CategoryId}");

            _context.TechTalk.Add(techTalk);
            _context.SaveChanges();  

            Console.WriteLine("TechTalk persisted successfully");
        }
    }    
}