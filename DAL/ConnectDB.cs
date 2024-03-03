using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class ConnectDB
    {
        private static string conStr = ConfigurationManager.ConnectionStrings["DatabaseConnection"].ConnectionString;
        private static string databaseName = MongoUrl.Create(conStr).DatabaseName;

        protected static IMongoCollection<T> GetCollection<T>(string collectionName)
        {
            MongoClient client = new MongoClient(conStr);
            IMongoDatabase database = client.GetDatabase(databaseName);
            IMongoCollection<T> collection = database.GetCollection<T>(collectionName);
            return collection;
        }
    }
}
