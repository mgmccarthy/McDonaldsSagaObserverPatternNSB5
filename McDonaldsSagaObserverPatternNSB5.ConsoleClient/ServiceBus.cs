using NServiceBus;

namespace McDonaldsSagaObserverPatternNSB5.ConsoleClient
{
    public static class ServiceBus
    {
        public static ISendOnlyBus Bus { get; private set; }

        private static readonly object padlock = new object();

        public static void Init()
        {
            if (Bus != null)
                return;

            lock (padlock)
            {
                if (Bus != null)
                    return;

                var cfg = new BusConfiguration();

                cfg.UseTransport<MsmqTransport>();
                cfg.UsePersistence<InMemoryPersistence>();
                cfg.EndpointName("McDonaldsSagaObserverPatternNSB5.ConsoleClient");
                cfg.PurgeOnStartup(true);
                cfg.EnableInstallers();

                Bus = NServiceBus.Bus.CreateSendOnly(cfg);
            }
        }
    }
}
