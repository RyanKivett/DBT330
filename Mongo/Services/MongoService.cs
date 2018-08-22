using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Mongo.Services
{
    public class MongoService
    {
        public delegate void ReturnDelegate(string output);
        public string Database { get; set; }
        public string Collection { get; set; }
        public MongoService(string database, string collection)
        {
            Database = database;
            Collection = collection;
        }

        public void FindOne(ReturnDelegate returnDelegate, string query)
        {
            String connection = "mongodb://localhost";
            MongoClient client = new MongoClient(connection);
            var database = client.GetDatabase(Database);
            var collection = database.GetCollection<BsonDocument>(Collection);

            var projection = Builders<BsonDocument>.Projection.Exclude("_id").Include("section_texts");

            var filter = Builders<BsonDocument>.Filter.Eq("title", "Alabama");
            var document = collection.Find(filter).Project(projection).FirstOrDefault();

            if (document == null)
            {
                Console.WriteLine("Article not found");
            }
            else
            {
                string output = "";
                foreach (var value in document.Values)
                {
                    output += value.ToString();
                }

                output = output.TrimStart(new char[] { '[', '\n' });
                output = output.TrimEnd(new char[] { ']', '\n' });

                returnDelegate(output);
            }
        }
    }
}
