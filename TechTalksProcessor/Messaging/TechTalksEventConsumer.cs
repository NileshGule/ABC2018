using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace TechTalksProcessor.Messaging
{
    public class TechTalksEventConsumer : ITechTalksEventConsumer
    {
        private const string exchangeName = "TechTalksExchange";
        private const string queueName = "hello";
        private const string routingKey = "hello";

        public void ConsumeMessage()
        {
            Console.WriteLine("Inside send message");
            // var factory = new ConnectionFactory() { HostName = "localhost" };
            // var factory = new ConnectionFactory() { HostName = "rabbitmq", Port = 31672 };
            var factory = new ConnectionFactory() { HostName = "rabbitmq"};

            Console.WriteLine("Inside connection factory");
        
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

                    
                    channel.QueueBind(queueName, exchangeName, routingKey);

                    var consumer = new EventingBasicConsumer(channel);

                    consumer.Received += (model, ea) =>
                    {
                        var body = ea.Body;
                        var message = Encoding.UTF8.GetString(body);
                        Console.WriteLine(" [x] {0}", message);
                    };

                    channel.BasicConsume(queue: queueName,
                                        autoAck: true,
                                        consumer: consumer);
                }
            }
        }
    }
    
}