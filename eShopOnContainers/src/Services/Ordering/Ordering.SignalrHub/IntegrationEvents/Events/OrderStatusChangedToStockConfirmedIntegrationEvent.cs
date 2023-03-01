﻿namespace Microsoft.eShopOnContainers.Services.Ordering.SignalrHub.IntegrationEvents.Events
{
    public record OrderStatusChangedToStockConfirmedIntegrationEvent : IntegrationEvent
    {
        public OrderStatusChangedToStockConfirmedIntegrationEvent(int orderId, string orderStatus, string buyerName)
        {
            OrderId = orderId;
            OrderStatus = orderStatus;
            BuyerName = buyerName;
        }

        public int OrderId { get; }
        public string OrderStatus { get; }
        public string BuyerName { get; }
    }
}
