using Crosscutting.Common.Data;
using Crosscutting.Domain.Bus;
using Crosscutting.Domain.Queries;
using ProjetoDDD.Domain.Interfaces.Repositories;
using ProjetoDDD.Domain.Models;
using ProjetoDDD.Domain.Queries;
using System.Threading.Tasks;

namespace ProjetoDDD.Domain.QueryHandlers
{
    public class GetPagedDemoQueryHandler : MediatorQueryHandler<GetPagedDemoQuery, PagedList<DemoModel>>
    {
        private readonly IDemoMongoRepository _demoRepository;

        public GetPagedDemoQueryHandler(
            IDemoMongoRepository demoRepository,
            IMediatorHandler mediator) : base(mediator)
        {
            _demoRepository = demoRepository;
        }

        public override async Task<PagedList<DemoModel>> AfterValidation(GetPagedDemoQuery request)
        {
            return await _demoRepository
                .GetAllPagedAsync(request.Restriction, request.Order, request.page);
        }
    }
}