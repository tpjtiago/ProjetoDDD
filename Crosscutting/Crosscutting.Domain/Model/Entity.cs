using Crosscutting.Common.Extensions;
using System;

namespace Crosscutting.Domain.Model
{
    public class Entity : IEntity
    {
        public Guid Id { get; set; }
        public DateTime DataCriacao { get; set; } = DateTime.Now.ToBrazilianTimezone();
    }
}