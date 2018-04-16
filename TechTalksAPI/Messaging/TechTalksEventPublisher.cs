using System;
using System.Text;
using RabbitMQ.Client;
using Newtonsoft.Json;
using TechTalksModel;
// using TechTalksAPI.Models;

namespace TechTalksAPI.Messaging
{
    public class TechTalksEventPublisher : ITechTalksEventPublisher
    {
        private const string exchangeName = "TechTalksExchange";
        private const string queueName = "hello";
        private const string routingKey = "hello";
        
        public void SendMessage(TechTalk talk)
        {
            Console.WriteLine("Inside send message");
        
            var factory = new ConnectionFactory() { HostName = "rabbitmq"};

            Console.WriteLine("Inside connection factory");
        
            using (var connection = factory.CreateConnection())
            {
                Console.WriteLine("Inside connection");

                using (var channel = connection.CreateModel())
                {
                    Console.WriteLine("Inside model");
                    channel.ExchangeDeclare(exchangeName, "fanout");
                    
                    TechTalksModel.TechTalk techTalk = new TechTalksModel.TechTalk
                    {
                        Id = talk.Id,
                        TechTalkName = talk.TechTalkName,
                        CategoryId = talk.CategoryId
                    };
                                        
                    var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(techTalk));                    

                    channel.BasicPublish(exchange: exchangeName,
                                        routingKey: routingKey,
                                        basicProperties: null,
                                        body: body);
                    Console.WriteLine(" [x] Sent {0}", techTalk);
                }
            }
        }
    }
}