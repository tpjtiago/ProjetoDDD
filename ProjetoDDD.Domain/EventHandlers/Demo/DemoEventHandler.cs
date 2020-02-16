using Crosscutting.Common.Extensions;
using Crosscutting.Domain.Interfaces.Repositories;
using Crosscutting.Domain.Model;
using ProjetoDDD.Domain.Events;
using ProjetoDDD.Domain.Interfaces.Events;
using ProjetoDDD.Domain.Interfaces.Repositories;
using MediatR;
using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace ProjetoDDD.Domain.EventHandlers
{
    /// <summary>
    /// Event Sourcing - deve ser utilizado para derivação do dado. Exp.: Mensageria, Log e etc.
    /// </summary>
    public class DemoEventHandler :
        INotificationHandler<AddedDemoEvent>,
        INotificationHandler<UpdatedDemoEvent>,
        INotificationHandler<RemovedDemoEvent>
    {
        private readonly IDemoMongoRepository _demoRepository;
        private readonly IEventStoreRepository _eventStoreRepository;
        private readonly ClaimsPrincipal _user;

        public DemoEventHandler(
            IDemoMongoRepository demoRepository,
            IEventStoreRepository eventStoreRepository,
            ClaimsPrincipal user)
        {
            _demoRepository = demoRepository;
            _eventStoreRepository = eventStoreRepository;
            _user = user;
        }

        public async Task Handle(AddedDemoEvent notification, CancellationToken cancellationToken)
        {
            await _demoRepository.AddAsync(notification.Demo);
            await ApplyEvent(notification);
        }

        public async Task Handle(UpdatedDemoEvent notification, CancellationToken cancellationToken)
        {
            await _demoRepository.UpdateAsync(notification.Demo.Id, notification.Demo);
            await ApplyEvent(notification);
        }

        public async Task Handle(RemovedDemoEvent notification, CancellationToken cancellationToken)
        {
            await _demoRepository.RemoveAsync(notification.Demo.Id);
            await ApplyEvent(notification);
        }

        private async Task ApplyEvent(IDemoEvent notification)
        {
            var eventStore = new EventStore()
            {
                Data = notification.Demo,
                Id = Guid.NewGuid(),
                StoreType = notification.GetType().Name,
                TimeStamp = DateTime.Now,
                UserId = _user.GetUserIdFromToken(),
                UserName = _user.GetUserNameFromToken()
            };
            await _eventStoreRepository.AddAsync(eventStore);
        }
    }
}