using ProjetoDDD.Domain.Models;

namespace ProjetoDDD.Domain.Queries
{
    public class GetDemoQuery : DemoQuery<DemoModel>
    {
        public override bool IsValid()
        {
            return true;
        }
    }
}