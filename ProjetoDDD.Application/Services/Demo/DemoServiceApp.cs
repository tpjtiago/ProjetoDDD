using AutoMapper;
using Crosscutting.Common.Data;
using Crosscutting.Domain.Bus;
using ProjetoDDD.Application.Interfaces;
using ProjetoDDD.Application.ViewModels;
using ProjetoDDD.Domain.Commands;
using ProjetoDDD.Domain.Queries;
using System;
using System.Threading.Tasks;

namespace ProjetoDDD.Application.Services
{
    public class DemoServiceApp : IDemoServiceApp
    {
        private readonly IMapper _mapper;
        private readonly IMediatorHandler _mediator;

        public DemoServiceApp(
            IMapper mapper,
            IMediatorHandler mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<PagedList<DemoViewModel>> GetPagedDemos(int page, int size,
            string orderProperty, bool orderCrescent, string filterProperty, string filterValue)
        {
            var query = new GetPagedDemoQuery
            {
                page = new Page(page, size),
                Order = new Order(orderProperty, orderCrescent),
                Restriction = new Restriction(filterProperty, Condition.Default, filterValue)
            };

            var demos = await _mediator.SendQuery(query);

            return _mapper.Map<PagedList<DemoViewModel>>(demos);
        }

        public async Task<DemoViewModel> GetDemo(Guid id)
        {
            var query = new GetDemoQuery { Id = id };
            var demo = await _mediator.SendQuery(query);

            return _mapper.Map<DemoViewModel>(demo);
        }

        public async Task Save(DemoViewModel model, bool update = false)
        {
            if (!update)
            {
                var command = _mapper.Map<AddDemoCommand>(model);
                await _mediator.SendCommand(command);
            }
            else
            {
                var command = _mapper.Map<UpdateDemoCommand>(model);
                await _mediator.SendCommand(command);
            }
        }

        public async Task Remove(Guid id)
        {
            var command = new RemoveDemoCommand(id);
            await _mediator.SendCommand(command);
        }
    }
}