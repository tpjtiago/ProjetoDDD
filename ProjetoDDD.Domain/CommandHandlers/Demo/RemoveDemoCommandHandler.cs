using Crosscutting.Domain.Bus;
using Crosscutting.Domain.Commands;
using ProjetoDDD.Domain.Commands;
using ProjetoDDD.Domain.Events;
using ProjetoDDD.Domain.Interfaces.Repositories;
using ProjetoDDD.Domain.Interfaces.UnitOfWork;
using System.Threading.Tasks;

namespace ProjetoDDD.Domain.CommandHandlers
{
    public class RemoveDemoCommandHandler : MediatorCommandHandler<RemoveDemoCommand>
    {
        private readonly IDemoPostgreRepository _demoRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RemoveDemoCommandHandler(
            IDemoPostgreRepository demoRepository,
            IUnitOfWork unitOfWork,
            IMediatorHandler mediator) : base(mediator)
        {
            _demoRepository = demoRepository;
            _unitOfWork = unitOfWork;
        }

        public override async Task AfterValidation(RemoveDemoCommand request)
        {
            var demo = await _demoRepository.GetFirstByExpressionAsync(x => x.Id == request.Id);

            if (demo == null)
            {
                NotifyError($"O registro com o código {request.Id} não existe");
                return;
            }

            await _demoRepository.DeleteByIdAsync(demo.Id);

            if (!HasNotification() && _unitOfWork.CommitAsync().Result)
                await _mediator.RaiseEvent(new RemovedDemoEvent(demo));
            else
                NotifyError("Commit", "Tivemos um problema ao tentar salvar seus dados.");
        }
    }
}