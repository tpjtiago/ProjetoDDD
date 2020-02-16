using Crosscutting.Domain.Bus;
using Crosscutting.Domain.Commands;
using ProjetoDDD.Domain.Commands;
using ProjetoDDD.Domain.Events;
using ProjetoDDD.Domain.Interfaces.Repositories;
using ProjetoDDD.Domain.Interfaces.UnitOfWork;
using ProjetoDDD.Domain.Models;
using System;
using System.Threading.Tasks;

namespace ProjetoDDD.Domain.CommandHandlers
{
    public class AddDemoCommandHandler : MediatorCommandHandler<AddDemoCommand>
    {
        private readonly IDemoPostgreRepository _demoRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AddDemoCommandHandler(
            IDemoPostgreRepository demoRepository,
            IUnitOfWork unitOfWork,
            IMediatorHandler mediator) : base(mediator)
        {
            _demoRepository = demoRepository;
            _unitOfWork = unitOfWork;
        }

        public override async Task AfterValidation(AddDemoCommand request)
        {
            var registered = await _demoRepository
                .ExistsByExpressionAsync(x => x.Description == request.Description);

            if (registered)
            {
                NotifyError("O registro já existe");
                return;
            }

            var newRecord = new DemoModel
            {
                Id = Guid.NewGuid(),
                Description = request.Description
            };

            await _demoRepository.InsertOrUpdateAsync(newRecord);

            if (!HasNotification() && _unitOfWork.CommitAsync().Result)
                await _mediator.RaiseEvent(new AddedDemoEvent(newRecord));
            else
                NotifyError("Commit", "Tivemos um problema ao tentar salvar seus dados.");
        }
    }
}