﻿using Crosscutting.Domain.Bus;
using Crosscutting.Domain.Commands;
using Crosscutting.Domain.Events;
using Crosscutting.Domain.Notifications;
using Crosscutting.Domain.Queries;
using MediatR;
using System;
using System.Threading.Tasks;

namespace Crosscutting.Bus
{
    public sealed class InMemoryBus : IMediatorHandler
    {
        private readonly IMediator _mediator;
        private readonly DomainNotificationHandler _domainNotification;

        public InMemoryBus(
            IMediator mediator,
            INotificationHandler<DomainNotification> domainNotification)
        {
            _mediator = mediator ?? throw new ArgumentException(nameof(mediator));
            _domainNotification = (DomainNotificationHandler)domainNotification ??
                throw new ArgumentException(nameof(domainNotification));
        }

        public Task SendCommand<T>(T command) where T : Command
        {
            return _mediator.Send(command);
        }

        public Task<TResponse> SendQuery<TResponse>(Query<TResponse> query) where TResponse : class
        {
            return _mediator.Send(query);
        }

        public Task RaiseEvent<T>(T @event) where T : Event
        {
            return _mediator.Publish(@event);
        }

        public INotificationHandler<DomainNotification> GetNotificationHandler()
        {
            return _domainNotification;
        }

        public bool HasNotification()
        {
            return _domainNotification.HasNotifications();
        }
    }
}