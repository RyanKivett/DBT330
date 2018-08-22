using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mongo.Services;

namespace Mongo
{
    class Program
    {
        static void Main(string[] args)
        {

            MongoService mongoService = new MongoService("Search_Wikipedia", "Total");
            mongoService.FindOne(Console.WriteLine, "Alabama");
        }

        
    }
}
