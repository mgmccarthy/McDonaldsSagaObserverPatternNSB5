using System;

namespace McDonaldsSagaObserverPatternNSB5.Messages.InternalMessages
{
    public class FriesCompleted
    {
        public Guid OrderId { get; set; }
    }
}
