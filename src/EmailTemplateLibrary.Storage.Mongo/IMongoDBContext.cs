using MongoDB.Driver;

namespace EmailTemplateLibrary.Storage.Mongo
{
    public interface IMongoDBContext
    {
        IMongoCollection<T> GetCollection<T>(string name);
    }
}

