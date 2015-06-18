namespace McDonaldsSagaObserverPatternNSB5.SagaEndpoint
{
    using NServiceBus;

    public class EndpointConfig : IConfigureThisEndpoint
    {
        public void Customize(BusConfiguration configuration)
        {
            configuration.UsePersistence<InMemoryPersistence>();
        }
    }
}
