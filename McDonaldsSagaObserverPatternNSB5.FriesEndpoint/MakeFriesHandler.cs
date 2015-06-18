using System.Threading;
using McDonaldsSagaObserverPatternNSB5.Messages.Commands;
using McDonaldsSagaObserverPatternNSB5.Messages.InternalMessages;
using NServiceBus;
using NServiceBus.Logging;

namespace McDonaldsSagaObserverPatternNSB5.FriesEndpoint
{
    public class MakeFriesHandler : IHandleMessages<MakeFries>
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(MakeFriesHandler));
        private readonly IBus bus;

        public MakeFriesHandler(IBus bus)
        {
            this.bus = bus;
        }

        public void Handle(MakeFries message)
        {
            Log.Warn("starting to make fries");
            Thread.Sleep(10000); //10 seconds
            Log.Warn("fries done");
            bus.Reply(new FriesCompleted { OrderId = message.OrderId });
        }
    }
}
