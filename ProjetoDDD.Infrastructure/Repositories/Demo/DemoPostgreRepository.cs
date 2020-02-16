using Crosscutting.Infrastructure.Repositories;
using ProjetoDDD.Domain.Interfaces.Repositories;
using ProjetoDDD.Domain.Models;
using ProjetoDDD.Infrastructure.Contexts;

namespace ProjetoDDD.Infrastructure.Repositories
{
    public class DemoPostgreRepository : PostgreRepository<DemoModel>, IDemoPostgreRepository
    {
        public DemoPostgreRepository(ProjetoDDDPostgreContext context)
            : base(context) { }
    }
}