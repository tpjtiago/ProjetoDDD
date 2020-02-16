using Crosscutting.Domain.Commands;
using Crosscutting.Domain.Events;
using Crosscutting.Domain.Notifications;
using Crosscutting.Domain.Queries;
using MediatR;
using System.Threading.Tasks;

namespace Crosscutting.Domain.Bus
{
    public interface IMediatorHandler
    {
        Task SendCommand<T>(T command) where T : Command;
        Task<TResponse> SendQuery<TResponse>(Query<TResponse> query) where TResponse : class;
        Task RaiseEvent<T>(T @event) where T : Event;
        bool HasNotification();
        INotificationHandler<DomainNotification> GetNotificationHandler();
    }
}