using MongoDB.Driver;
using System.Security.Authentication;

namespace EmailTemplateLibrary.Storage.Mongo
{
    public class MongoDBContext<T> : IMongoDBContext where T : class
    {
        private IMongoDatabase _db { get; set; }
        private MongoClient _mongoClient { get; set; }
        public MongoDBContext(string connectionString, string databaseName = "templates")
        {
            MongoClientSettings settings = MongoClientSettings.FromUrl(new MongoUrl(connectionString));
            settings.SslSettings = new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 };
            _mongoClient = new MongoClient(settings);            
            _db = _mongoClient.GetDatabase(databaseName);
        }

        public IMongoCollection<T> GetCollection<T>(string name)
        {
            return _db.GetCollection<T>(name);
        }
    }
}

