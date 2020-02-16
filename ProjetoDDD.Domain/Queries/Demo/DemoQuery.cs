using Crosscutting.Common.Data;
using Crosscutting.Domain.Queries;
using System;

namespace ProjetoDDD.Domain.Queries
{
    public abstract class DemoQuery<TResponse> : Query<TResponse>
    {
        public Guid Id { get; set; }
        public Page page { get; set; }
        public Order Order { get; set; }
        public Restriction Restriction { get; set; }
    }
}