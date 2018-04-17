using Nest;
using System;
using TechTalksELKProcessor.Documents;

namespace TechTalksELKProcessor.Indexer
{
    public class Index
    {
        private const string IndexName = "techtalks_index";
        public static void Setup()
        {
            var client = GetElasticClient();

            // client.DeleteIndex(IndexName);

            if (!client.IndexExists(IndexName).Exists)
            {
                Console.WriteLine("Creating new Index");                

                client.CreateIndex(IndexName);
            }
            
            Console.WriteLine("Initial setup completed with Index creation");
        }

        public static void CreateDocument(TechTalk techTalk)
        {
            try
            {
                var client = GetElasticClient();

                var result = client.Index(techTalk, idx => idx.Index(IndexName));

                Console.WriteLine($"Result : {result.IsValid}");

                if(!result.IsValid)
                {
                    Console.WriteLine(result.DebugInformation);
                }                
                
            }
            catch (System.Exception)
            {
                Console.WriteLine($"Index {IndexName} failed");
            }
        }

        private static ElasticClient GetElasticClient()
        {
            var node = new Uri("http://elk:9200/");
            
            var settings = new ConnectionSettings(node);
            var client = new ElasticClient(settings);

            return client;
        }
    }
}