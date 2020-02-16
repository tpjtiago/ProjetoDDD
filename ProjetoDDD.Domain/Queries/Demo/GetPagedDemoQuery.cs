using Crosscutting.Common.Data;
using ProjetoDDD.Domain.Models;

namespace ProjetoDDD.Domain.Queries
{
    public class GetPagedDemoQuery : DemoQuery<PagedList<DemoModel>>
    {
        public override bool IsValid()
        {
            return true;
        }
    }
}