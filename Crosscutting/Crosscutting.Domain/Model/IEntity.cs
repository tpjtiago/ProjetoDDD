using System;

namespace Crosscutting.Domain.Model
{
    public interface IEntity
    {
        Guid Id { get; set; }
    }
}