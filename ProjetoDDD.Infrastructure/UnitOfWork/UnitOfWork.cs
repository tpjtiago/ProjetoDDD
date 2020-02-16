using ProjetoDDD.Domain.Interfaces.UnitOfWork;
using ProjetoDDD.Infrastructure.Contexts;
using System.Threading.Tasks;

namespace ProjetoDDD.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ProjetoDDDPostgreContext _context;

        public UnitOfWork(ProjetoDDDPostgreContext context)
        {
            _context = context;
        }

        public async Task<bool> CommitAsync()
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                bool success = (await _context.SaveChangesAsync()) > 0;

                if (success)
                    await transaction.CommitAsync();
                else
                    await transaction.RollbackAsync();

                return success;
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}