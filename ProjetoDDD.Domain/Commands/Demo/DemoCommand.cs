using Crosscutting.Domain.Commands;
using System;

namespace ProjetoDDD.Domain.Commands
{
    public abstract class DemoCommand : Command
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
    }
}