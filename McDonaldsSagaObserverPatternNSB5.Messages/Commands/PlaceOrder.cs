using System;
using System.Collections.Generic;

namespace McDonaldsSagaObserverPatternNSB5.Messages.Commands
{
    public class PlaceOrder
    {
        public Guid OrderId { get; set; }
        public List<string> MenuItems { get; set; }
    }
}
