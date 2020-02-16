using Crosscutting.Infrastructure.Contexts.MongoDb;
using Crosscutting.Infrastructure.Repositories;
using ProjetoDDD.Domain.Models;

namespace ProjetoDDD.Test.Repositories
{
    public class DemoRepository : MongoRepository<DemoModel>
    {
        public DemoRepository(string collectionName, IMongoDbContext context)
            : base(collectionName, context) { }
    }
}