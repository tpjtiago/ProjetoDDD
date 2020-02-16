using Crosscutting.Domain.Interfaces.Repositories;
using ProjetoDDD.Domain.Models;

namespace ProjetoDDD.Domain.Interfaces.Repositories
{
    /// <summary>
    /// Interce do repositório herdando o uso do repositório genérico da crosscutting
    /// </summary>
    public interface IDemoMongoRepository : IMongoDbRepository<DemoModel>
    {
    }
}