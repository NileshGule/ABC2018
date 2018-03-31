using System;
using System.Text;
using RabbitMQ.Client;

namespace TechTalksAPI.Messaging
{
    public class TechTalksEventPublisher : ITechTalksEventPublisher
    {
        private const string exchangeName = "TechTalksExchange";
        private const string queueName = "hello";
        private const string routingKey = "hello";
        public void SendMessage()
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

                    string message = "Hello World!";
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.QueueBind(queueName, exchangeName, routingKey);

                    channel.BasicPublish(exchange: exchangeName,
                                        routingKey: routingKey,
                                        basicProperties: null,
                                        body: body);
                    Console.WriteLine(" [x] Sent {0}", message);
                }
            }
        }
    }
}