using MongoDB.Driver;

namespace Crosscutting.Infrastructure.Contexts.MongoDb
{
    public interface IEventStoreContext
    {
        IMongoDatabase Database { get; }
        IMongoCollection<T> GetCollection<T>(string collectionName);
        IMongoClient Client { get; }
    }
}