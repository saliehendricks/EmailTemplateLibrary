using EmailTemplateLibrary.Model;
using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System.Security.Authentication;

namespace EmailTemplateLibrary.Storage.Mongo
{
    public interface IMongoDBContext
    {
        IMongoCollection<T> GetCollection<T>(string name);
    }

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

    public class MongoTemplateStorage : TemplateStorage
    {
        protected readonly IMongoDBContext _mongoContext;
        protected IMongoCollection<Template> _dbCollection;
        protected string _collectionName = "templatestore";

        public MongoTemplateStorage(MongoStorageOptions options)
        {
            _mongoContext = new MongoDBContext<Template>(options.UrlConnection);
            _dbCollection = _mongoContext.GetCollection<Template>(_collectionName);
        }

        public override string GetTemplate(string keyName)
        {
            string result = "";
            var filterDefinition = Builders<Template>.Filter.Where(x => x.TemplateKey == keyName);

            var entities = _dbCollection.Find(filterDefinition);
            var template = entities.FirstOrDefault();
            if (template != null)
            {
                result = template.TemplateText;
            }
            return result;
        }

        public override List<Template> GetTemplates(string qry = "")
        {
            List<Template> templates = new List<Template>();
            //if (Directory.Exists(_folderPath))
            //{
            //    var files = Directory.GetFiles(_folderPath);
            //    foreach (var file in files)
            //    {
            //        FileInfo fi = new FileInfo(file);
            //        templates.Add(new Template()
            //        {
            //            TemplateKey = fi.Name,
            //            TemplateText = File.ReadAllText(Path.Combine(_folderPath, fi.Name)),
            //            Created = fi.CreationTime,
            //            Modified = fi.LastWriteTime,
            //            Md5Hash = "TODO"
            //        });
            //    }
            //}
            return templates;
        }

        public override void SaveTemplate(string keyName, string content)
        {
            var t = new Template()
            {
                TemplateKey = keyName,
                TemplateText = content,
                Created = DateTime.Now,
                Modified = DateTime.Now,
                Md5Hash = ""
            };

            var filterDefinition = Builders<Template>.Filter.Where(x=>x.TemplateKey == keyName);

            var entities = _dbCollection.Find(filterDefinition);
            var template = entities.FirstOrDefault();
            if (template == null)
            {
                _dbCollection.ReplaceOne(filterDefinition, t);
            }
            else 
            {
                _dbCollection.InsertOne(t);
            }
            
        }

        public override void DeleteTemplate(string keyName)
        {
            var filterDefinition = Builders<Template>.Filter.Where(x => x.TemplateKey == keyName);
            _dbCollection.DeleteOne(filterDefinition);
        }

        public override void CreateBaseTemplates()
        {
            string registerEmail = BaseTemplates.BaseTemplates.RegistrationActivationEmailTemplate();
            SaveTemplate("registerEmail", registerEmail);
        }

    }
}

