using Crosscutting.Infrastructure.Contexts.MongoDb;
using ProjetoDDD.Test.Repositories;
using MongoDB.Driver;
using System;

namespace ProjetoDDD.Test.Common
{
    public class FakeMongoContext : IDisposable
    {
        private readonly string databaseSuffix;
        private readonly string collection;

        public MongoDbContext Context { get; }
        public IMongoDatabase Database { get; }

        public FakeMongoContext(string collectionName)
        {
            var mongoUrl = ConfigFactory.BuildConfig()["MongoDb:ConnectionString"];

            databaseSuffix = Guid.NewGuid().ToString();
            collection = $"{collectionName}{databaseSuffix}";

            Context = new MongoDbContext(
                new MongoDbContextConfig { ConnectionString = mongoUrl, Database = ConfigFactory.BuildConfig()["MongoDb:Database"]},
                new MongoClient(mongoUrl));

            Database = Context.Database;
        }

        public DemoRepository BuildDemoRepository() => new DemoRepository(collection, Context);

        public void Dispose()
        {
            Database.DropCollection(collection);
            Context.Client.DropDatabase(ConfigFactory.BuildConfig()["MongoDb:Database"]);
        }

        public void RemoveCollections()
        {
            Dispose();
        }
    }
}