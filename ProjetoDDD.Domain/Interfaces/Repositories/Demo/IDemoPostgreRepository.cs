using Crosscutting.Domain.Interfaces.Repositories;
using ProjetoDDD.Domain.Models;

namespace ProjetoDDD.Domain.Interfaces.Repositories
{
    public interface IDemoPostgreRepository : IPostgreRepository<DemoModel>
    {
    }
}