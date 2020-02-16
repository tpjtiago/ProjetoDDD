using Crosscutting.Domain.Bus;
using Crosscutting.Domain.Queries;
using ProjetoDDD.Domain.Interfaces.Repositories;
using ProjetoDDD.Domain.Models;
using ProjetoDDD.Domain.Queries;
using System.Threading.Tasks;

namespace ProjetoDDD.Domain.QueryHandlers
{
    public class GetDemoQueryHandler : MediatorQueryHandler<GetDemoQuery, DemoModel>
    {
        private readonly IDemoMongoRepository _demoRepository;

        public GetDemoQueryHandler(
            IDemoMongoRepository demoRepository,
            IMediatorHandler mediator) : base(mediator)
        {
            _demoRepository = demoRepository;
        }

        public override async Task<DemoModel> AfterValidation(GetDemoQuery request)
        {
            return await _demoRepository.GetOneAsync(x => x.Id == request.Id);
        }
    }
}