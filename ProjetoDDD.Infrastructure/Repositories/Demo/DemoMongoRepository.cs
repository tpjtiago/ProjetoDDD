using Crosscutting.Infrastructure.Contexts.MongoDb;
using Crosscutting.Infrastructure.Repositories;
using ProjetoDDD.Domain.Interfaces.Repositories;
using ProjetoDDD.Domain.Models;

namespace ProjetoDDD.Infrastructure.Repositories
{
    /// <summary>
    /// Repositório herdando o uso do repositório genérico da crosscutting
    /// </summary>
    public class DemoMongoRepository : MongoRepository<DemoModel>, IDemoMongoRepository
    {
        public DemoMongoRepository(IMongoDbContext context, string collectionName = "Demo")
            : base(collectionName, context) { }
    }
}