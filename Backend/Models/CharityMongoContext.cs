using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Backend.Models
{
    public class CharityMongoContext
    {
        protected MongoDatabase Database { get; set; }

        public MongoCollection<Subscription> Subscriptions
        {
            get
            {
                return this.Database.GetCollection<Subscription>("subscriptions");
            }
        }

        public CharityMongoContext()
        {
            var client = new MongoClient(ConfigurationManager.ConnectionStrings["DefaultMongoConnectionString"].ConnectionString);
            var server = client.GetServer();
            Database = server.GetDatabase(ConfigurationManager.AppSettings["DefaultDatabaseName"]);
        }
    }
}