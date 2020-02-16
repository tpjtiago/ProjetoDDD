using ProjetoDDD.Domain.Models;

namespace ProjetoDDD.Domain.Interfaces.Events
{
    public interface IDemoEvent
    {
        DemoModel Demo { get; set; }
    }
}