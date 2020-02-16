using Crosscutting.Domain.Events;
using ProjetoDDD.Domain.Interfaces.Events;
using ProjetoDDD.Domain.Models;

namespace ProjetoDDD.Domain.Events
{
    public class RemovedDemoEvent : Event, IDemoEvent
    {
        public RemovedDemoEvent(DemoModel demo)
        {
            Demo = demo;
        }

        public DemoModel Demo { get; set; }
    }
}