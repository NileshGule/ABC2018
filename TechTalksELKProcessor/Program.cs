using System;
using RabbitMQ.Client;
using System.Threading;
using TechTalksELKProcessor.Documents;
using TechTalksELKProcessor.Indexer;

namespace TechTalksELKProcessor
{
    class Program
    {
        private static ManualResetEvent _ResetEvent = new ManualResetEvent(false);

        private const string QUEUE_GROUP = "index-handler";

        static void Main(string[] args)
        {
            Console.WriteLine("Initializing Elasticsearch. url: {0}", "http://localhost:9200");
            Index.Setup();

            TechTalk talk = new TechTalk
            {
                Id = 4,
                TechTalkName = "Docker",
                CategoryId = 2,
                EventTime = DateTime.UtcNow
            };

            Index.CreateDocument(talk);
            Console.WriteLine("Index written successfully");
        }
    }
}
