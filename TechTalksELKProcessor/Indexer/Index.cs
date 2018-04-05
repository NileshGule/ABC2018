using Nest;
using System;
using TechTalksELKProcessor.Documents;

namespace TechTalksELKProcessor.Indexer
{
    public class Index
    {
        private const string IndexName = "techtalks";
        public static void Setup()
        {
            var node = new Uri("http://localhost:9200/");
            
            var settings = new ConnectionSettings(node);
            var client = new ElasticClient(settings);

            // client.DeleteIndex(IndexName);

            if (!client.IndexExists(IndexName).Exists)
            {
                Console.WriteLine("Creating new Index");
                

                client.CreateIndex(IndexName);
            }

            // client.CreateIndex("default", i => i
            //         .Mappings(m => m
            //             .Map<TechTalk>(ms => ms.AutoMap())));

            
            Console.WriteLine("Initial setup completed with Index creation");
            
        }

        public static void CreateDocument(TechTalk techTalk)
        {
            try
            {
                var node = new Uri("http://localhost:9200/");
            
                var settings = new ConnectionSettings(node)
                .DefaultIndex(IndexName);
                            
                var client = new ElasticClient(settings);
                var result = client.Index(techTalk, idx => idx.Index(IndexName));

                Console.WriteLine($"Result : {result.IsValid}");

                if(!result.IsValid)
                {
                    Console.WriteLine(result.DebugInformation);
                }
                

                var searchResults = client.Search<TechTalk>(s=>s
                                        .AllIndices()
                                    );
                
                Console.WriteLine($"Number of Indices found {searchResults.Documents.Count}");
                
            }
            catch (System.Exception)
            {
                Console.WriteLine("Index techTalks failed");
            }
        }
    }
}