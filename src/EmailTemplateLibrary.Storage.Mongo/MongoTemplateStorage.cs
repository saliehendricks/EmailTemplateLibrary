using EmailTemplateLibrary.Model;
using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Driver;
using System.Linq;

namespace EmailTemplateLibrary.Storage.Mongo
{
    public class MongoTemplateStorage : TemplateStorage
    {
        protected readonly IMongoDBContext _mongoContext;
        protected IMongoCollection<TemplateDto> _dbCollection;
        protected string _collectionName = "templatestore";

        public MongoTemplateStorage(MongoStorageOptions options)
        {
            _mongoContext = new MongoTemplateDBContext<TemplateDto>(options.UrlConnection);
            _dbCollection = _mongoContext.GetCollection<TemplateDto>(_collectionName);
        }

        public override string GetTemplate(string keyName)
        {
            string result = "";
            var filterDefinition = Builders<TemplateDto>.Filter.Where(x => x.TemplateKey == keyName);

            var entities = _dbCollection.Find(filterDefinition);
            var template = entities.FirstOrDefault();
            if (template != null)
            {
                result = template.TemplateText;
            }
            return result;
        }

        public override List<Template> GetTemplates(string qryKey = "")
        {
            var filterDefinition = qryKey == "" ? Builders<TemplateDto>.Filter.Empty : Builders<TemplateDto>.Filter.Where(x => x.TemplateKey.Contains(qryKey));
            var entities = _dbCollection.Find(filterDefinition);
            var templates = entities.ToList().Select(x=> x as Template).ToList();
            return templates;
        }

        public override void SaveTemplate(string keyName, string content)
        {
            var t = new TemplateDto()
            {
                TemplateKey = keyName,
                TemplateText = content,
                Created = DateTime.Now,
                Modified = DateTime.Now,
                Md5Hash = ""
            };

            var filterDefinition = Builders<TemplateDto>.Filter.Where(x=>x.TemplateKey == keyName);

            var entities = _dbCollection.Find(filterDefinition);
            var template = entities.FirstOrDefault();
            
            if (template != null)
            {
                t.Id = template.Id;
                _dbCollection.ReplaceOne(filterDefinition, t);
            }
            else 
            {
                t.Id = Guid.NewGuid().ToString();
                _dbCollection.InsertOne(t);
            }
            
        }

        public override void DeleteTemplate(string keyName)
        {
            var filterDefinition = Builders<TemplateDto>.Filter.Where(x => x.TemplateKey == keyName);
            _dbCollection.DeleteOne(filterDefinition);
        }

        public override void CreateBaseTemplates()
        {
            string registerEmail = BaseTemplates.BaseTemplates.RegistrationActivationEmailTemplate();
            SaveTemplate("registerEmail", registerEmail);
        }

    }
}

