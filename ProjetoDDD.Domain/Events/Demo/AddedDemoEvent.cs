using Crosscutting.Domain.Events;
using ProjetoDDD.Domain.Interfaces.Events;
using ProjetoDDD.Domain.Models;

namespace ProjetoDDD.Domain.Events
{
    public class AddedDemoEvent : Event, IDemoEvent
    {
        public AddedDemoEvent(DemoModel demo)
        {
            Demo = demo;
        }

        public DemoModel Demo { get; set; }
    }
}