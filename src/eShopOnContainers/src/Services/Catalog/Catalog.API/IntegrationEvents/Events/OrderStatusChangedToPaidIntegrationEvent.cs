﻿namespace Microsoft.eShopOnContainers.Services.Catalog.API.IntegrationEvents.Events
{
    public record OrderStatusChangedToPaidIntegrationEvent : IntegrationEvent
    {
        public OrderStatusChangedToPaidIntegrationEvent(int orderId,
            IEnumerable<OrderStockItem> orderStockItems)
        {
            OrderId = orderId;
            OrderStockItems = orderStockItems;
        }

        public int OrderId { get; }
        public IEnumerable<OrderStockItem> OrderStockItems { get; }
    }
}
