﻿namespace Webhooks.API.IntegrationEvents
{
    public class OrderStatusChangedToShippedIntegrationEventHandler : IIntegrationEventHandler<OrderStatusChangedToShippedIntegrationEvent>
    {
        private readonly ILogger _logger;
        private readonly IWebhooksRetriever _retriever;
        private readonly IWebhooksSender _sender;

        public OrderStatusChangedToShippedIntegrationEventHandler(IWebhooksRetriever retriever,
            IWebhooksSender sender,
            ILogger<OrderStatusChangedToShippedIntegrationEventHandler> logger)
        {
            _retriever = retriever;
            _sender = sender;
            _logger = logger;
        }

        public async Task Handle(OrderStatusChangedToShippedIntegrationEvent @event)
        {
            var subscriptions = await _retriever.GetSubscriptionsOfType(WebhookType.OrderShipped);
            _logger.LogInformation(
                "Received OrderStatusChangedToShippedIntegrationEvent and got {SubscriptionCount} subscriptions to process",
                subscriptions.Count());
            var whook = new WebhookData(WebhookType.OrderShipped, @event);
            await _sender.SendAll(subscriptions, whook);
        }
    }
}
