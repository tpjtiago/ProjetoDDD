﻿using Crosscutting.Common.Extensions;
using MediatR;
using System;

namespace Crosscutting.Domain.Events
{
    public abstract class Event : CommandMessage, INotification
    {
        public DateTime Timestamp { get; private set; }

        protected Event()
        {
            Timestamp = DateTime.Now.ToBrazilianTimezone();
        }
    }
}