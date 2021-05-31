using EmailTemplateLibrary.Model;
using MongoDB.Bson.Serialization.Attributes;

namespace EmailTemplateLibrary.Storage.Mongo
{
    public class TemplateDto : Template
    {
        [BsonId]
        public string Id { get; set; }
    }
}

