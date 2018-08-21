using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Mongo
{
    class Program
    {
        static void Main(string[] args)
        {
            String connection = "mongodb://localhost";
            MongoClient client = new MongoClient(connection);
            var database = client.GetDatabase("Search_Wikipedia");
            var collection = database.GetCollection<BsonDocument>("Total");

            Console.Write("Enter a title to search for: ");
            String query = Console.ReadLine();

            FindOne(collection, query);

            //Console.WriteLine(document.Values.ToString());

            //foreach(var document in documents)
            //{
            //    Console.WriteLine(document.ToString());
            //}
        }

        public static void FindOne(IMongoCollection<BsonDocument> collection, string query)
        {
            var projection = Builders<BsonDocument>.Projection.Exclude("_id").Include("section_texts");

            var filter = Builders<BsonDocument>.Filter.Eq("title", "Alabama");
            var document = collection.Find(filter).Project(projection).FirstOrDefault();

            if(document == null)
            {
                Console.WriteLine("Article not found");
            }
            else
            {
                string output;
                foreach (var value in document.Values)
                {
                    Console.WriteLine(value.ToString());
                }
            }
        }
    }
}
