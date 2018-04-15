﻿using System;
using System.Text;
using RabbitMQ.Client;
using Newtonsoft.Json;
using TechTalksModel;
using TechTalksAPI.Models;

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
        
            var factory = new ConnectionFactory() { HostName = "rabbitmq"};

            Console.WriteLine("Inside connection factory");
        
            using (var connection = factory.CreateConnection())
            {
                Console.WriteLine("Inside connection");

                using (var channel = connection.CreateModel())
                {

                    Console.WriteLine("Inside model");
                    channel.ExchangeDeclare(exchangeName, "fanout");
                    
                    string message = "Hello World!";
                    var techTalk = new TechTalksModel.TechTalk
                    {
                        Id = new Random().Next(100) ,
                        Name = "Azure bootcamp",
                        Category = new Random().Next(1,5)
                    };
                    
                    var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(techTalk));                    

                    channel.BasicPublish(exchange: exchangeName,
                                        routingKey: routingKey,
                                        basicProperties: null,
                                        body: body);
                    Console.WriteLine(" [x] Sent {0}", message);
                }
            }
        }

        public void SendMessage(Models.TechTalk talk)
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
                        Name = talk.TechTalkName,
                        Category = talk.CategoryId
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