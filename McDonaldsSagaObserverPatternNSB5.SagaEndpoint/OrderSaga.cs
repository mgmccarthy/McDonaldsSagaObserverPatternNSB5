using System;
using System.Collections.Generic;
using System.Linq;
using McDonaldsSagaObserverPatternNSB5.Messages.Commands;
using McDonaldsSagaObserverPatternNSB5.Messages.Events;
using McDonaldsSagaObserverPatternNSB5.Messages.InternalMessages;
using NServiceBus;
using NServiceBus.Logging;
using NServiceBus.Saga;

namespace McDonaldsSagaObserverPatternNSB5.SagaEndpoint
{
    public class OrderSaga : Saga<OrderSaga.SagaData>,
        IAmStartedByMessages<PlaceOrder>,
        IHandleMessages<FriesCompleted>,
        IHandleMessages<ShakeCompleted>
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(OrderSaga));

        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<SagaData> mapper)
        {
            mapper.ConfigureMapping<PlaceOrder>(msg => msg.OrderId).ToSaga(data => data.OrderId);
            mapper.ConfigureMapping<FriesCompleted>(msg => msg.OrderId).ToSaga(data => data.OrderId);
            mapper.ConfigureMapping<ShakeCompleted>(msg => msg.OrderId).ToSaga(data => data.OrderId);
        }

        public void Handle(PlaceOrder message)
        {
            Log.Warn(" order placed.");
            Data.OrderId = message.OrderId;
            Data.OrderList.AddRange(message.MenuItems);

            foreach (var item in message.MenuItems)
            {
                if(item == "Fries")
                    Bus.Send(new MakeFries { OrderId = message.OrderId });
                if(item == "Shake")
                    Bus.Send(new MakeShake { OrderId = message.OrderId });
            }

            Log.Warn(" order sent to all pertinenet stations.");
        }

        public void Handle(FriesCompleted message)
        {
            Log.Warn(" FriesCompleted");
            RemoveMenuItemFromOrderList("Fries");
        }

        public void Handle(ShakeCompleted message)
        {
            Log.Warn(" ShakeCompleted");
            RemoveMenuItemFromOrderList("Shake");
        }

        private void RemoveMenuItemFromOrderList(string menuItem)
        {
            Log.Warn(string.Format(" removing menu item {0} from order list.", menuItem));
            Data.OrderList.Remove(menuItem);
            if (TheOrderIsComplete())
                PublishOrderFinishedAndMarkSagaAsComplete();
        }

        private bool TheOrderIsComplete()
        {
            return !Data.OrderList.Any();
        }

        private void PublishOrderFinishedAndMarkSagaAsComplete()
        {
            Bus.Publish(new OrderCompleted { OrderId = Data.OrderId });
            MarkAsComplete();
            Log.Warn(" OrderCompleted and SagaComplete");
        }

        public class SagaData : IContainSagaData
        {
            public Guid Id { get; set; }
            public string Originator { get; set; }
            public string OriginalMessageId { get; set; }

            [Unique]
            public Guid OrderId { get; set; }
            public List<string> OrderList { get; set; }

            public SagaData()
            {
                OrderList = new List<string>();
            }
        }
    }
}
