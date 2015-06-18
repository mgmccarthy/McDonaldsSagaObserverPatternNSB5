using NServiceBus;

namespace McDonaldsSagaObserverPatternNSB5.FriesEndpoint
{
    public class EndpointConfig : IConfigureThisEndpoint
    {
        public void Customize(BusConfiguration configuration)
        {
            configuration.UsePersistence<InMemoryPersistence>();
        }
    }
}
