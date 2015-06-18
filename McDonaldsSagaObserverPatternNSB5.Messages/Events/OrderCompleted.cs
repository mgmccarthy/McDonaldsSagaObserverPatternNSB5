using System;

namespace McDonaldsSagaObserverPatternNSB5.Messages.Events
{
    public class OrderCompleted
    {
        public Guid OrderId { get; set; }
    }
}
