using Crosscutting.Domain.Events;
using ProjetoDDD.Domain.Interfaces.Events;
using ProjetoDDD.Domain.Models;

namespace ProjetoDDD.Domain.Events
{
    public class UpdatedDemoEvent : Event, IDemoEvent
    {
        public UpdatedDemoEvent(DemoModel demo)
        {
            Demo = demo;
        }

        public DemoModel Demo { get; set; }
    }
}