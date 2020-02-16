using FluentValidation.Results;
using Crosscutting.Common.Data;
using Crosscutting.Common.Extensions;
using Crosscutting.Domain.Events;
using System;

namespace Crosscutting.Domain.Queries
{
    public abstract class Query<TResponse> : QueryMessage<TResponse>
    {
        public DateTime Timestamp { get; private set; }
        public ValidationResult ValidationResult { get; set; } = new ValidationResult();

        protected Query()
        {
            Timestamp = DateTime.Now.ToBrazilianTimezone();
        }

        public abstract bool IsValid();
    }
}