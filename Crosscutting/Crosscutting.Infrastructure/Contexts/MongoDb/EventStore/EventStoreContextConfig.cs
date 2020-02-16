namespace Crosscutting.Infrastructure.Contexts.MongoDb
{
    public class EventStoreContextConfig
    {
        public string EventStoreConnectionString { get; set; }
        public string EventStoreDatabase { get; set; }
    }
}