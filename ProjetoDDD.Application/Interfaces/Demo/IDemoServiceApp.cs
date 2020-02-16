using Crosscutting.Common.Data;
using ProjetoDDD.Application.ViewModels;
using System;
using System.Threading.Tasks;

namespace ProjetoDDD.Application.Interfaces
{
    public interface IDemoServiceApp
    {
        Task<PagedList<DemoViewModel>> GetPagedDemos(int page, int size, string orderProperty,
            bool orderCrescent, string filterProperty, string filterValue);
        Task<DemoViewModel> GetDemo(Guid id);
        Task Save(DemoViewModel model, bool update = false);
        Task Remove(Guid id);
    }
}