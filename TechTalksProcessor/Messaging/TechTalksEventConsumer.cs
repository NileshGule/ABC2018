using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Newtonsoft.Json;
using TechTalksModel;

namespace TechTalksProcessor.Messaging
{
    public class TechTalksEventConsumer : ITechTalksEventConsumer
    {
        private const string exchangeName = "TechTalksExchange";
        private const string queueName = "hello";
        private const string routingKey = "hello";

        public void ConsumeMessage()
        {
            var factory = new ConnectionFactory() { HostName = "rabbitmq"};

            using (var connection = factory.CreateConnection())
            {
                Console.WriteLine("Inside connection");

                using (var channel = connection.CreateModel())
                {
                    Console.WriteLine("Inside model");
                    channel.ExchangeDeclare(exchangeName, "direct");
                    
                    channel.QueueDeclare(queue: queueName,
                                    durable: true,
                                    exclusive: false,
                                    autoDelete: false,
                                    arguments: null);

                    channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);
                    
                    channel.QueueBind(queueName, exchangeName, routingKey);

                    var consumer = new EventingBasicConsumer(channel);
                    Console.WriteLine("Created consumer...");
                    consumer.Received += (model, ea) =>
                    {
                        Console.WriteLine("Inside received...");
                        var body = ea.Body;
                        var message = Encoding.UTF8.GetString(body);
                        var techTalk = JsonConvert.DeserializeObject<TechTalk>(message);
                        Console.WriteLine($"Received message {message}");

                        Console.WriteLine($"Tech Talk Id : {techTalk.Id}");
                        Console.WriteLine($"Tech Talk Name : {techTalk.Name}");
                        Console.WriteLine($"Categor : {techTalk.Category}");
                    };

                    BasicGetResult result = channel.BasicGet(queueName, true);
                    if (result != null)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Inside received...");
                        string message = Encoding.UTF8.GetString(result.Body);
                         var techTalk = JsonConvert.DeserializeObject<TechTalk>(message);
                        Console.WriteLine($"Received message {message}");

                        Console.WriteLine($"Tech Talk Id : {techTalk.Id}");
                        Console.WriteLine($"Tech Talk Name : {techTalk.Name}");
                        Console.WriteLine($"Categor : {techTalk.Category}");

                        // _customerRepository.Insert(message);
                        Console.ResetColor();
                    }

                    channel.BasicConsume(queue: queueName,
                                        autoAck: true,
                                        consumer: consumer);
                }
            }
        }
    }
    
}