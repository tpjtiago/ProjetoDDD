using System;
using System.Threading.Tasks;

namespace ProjetoDDD.Domain.Interfaces.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        Task<bool> CommitAsync();
    }
}