using FluentValidation.Results;
using Crosscutting.Common.Extensions;
using Crosscutting.Domain.Events;
using System;

namespace Crosscutting.Domain.Commands
{
    public abstract class Command : CommandMessage
    {
        public DateTime Timestamp { get; private set; }
        public ValidationResult ValidationResult { get; set; } = new ValidationResult();

        protected Command()
        {
            Timestamp = DateTime.Now.ToBrazilianTimezone();
        }

        public abstract bool IsValid();
    }
}