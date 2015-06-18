using System;

namespace McDonaldsSagaObserverPatternNSB5.Messages.InternalMessages
{
    public class ShakeCompleted
    {
        public Guid OrderId { get; set; }
    }
}
