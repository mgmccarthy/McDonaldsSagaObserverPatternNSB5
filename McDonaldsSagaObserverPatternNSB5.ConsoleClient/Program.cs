using System;
using System.Collections.Generic;
using McDonaldsSagaObserverPatternNSB5.Messages.Commands;

namespace McDonaldsSagaObserverPatternNSB5.ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceBus.Init();
            Console.WriteLine("Press 'Enter' to place an Order. To exit, Ctrl + C");
            while (Console.ReadLine() != null)
            {
                var placeOrder = new PlaceOrder { OrderId = Guid.NewGuid(), MenuItems = new List<string> { "Fries", "Shake" } };
                ServiceBus.Bus.Send(placeOrder);
            }
        }
    }
}
