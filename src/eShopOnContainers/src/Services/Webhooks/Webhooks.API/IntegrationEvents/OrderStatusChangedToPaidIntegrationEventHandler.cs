﻿namespace Webhooks.API.IntegrationEvents
{
    public class OrderStatusChangedToPaidIntegrationEventHandler : IIntegrationEventHandler<OrderStatusChangedToPaidIntegrationEvent>
    {
        private readonly ILogger _logger;
        private readonly IWebhooksRetriever _retriever;
        private readonly IWebhooksSender _sender;

        public OrderStatusChangedToPaidIntegrationEventHandler(IWebhooksRetriever retriever,
            IWebhooksSender sender,
            ILogger<OrderStatusChangedToShippedIntegrationEventHandler> logger)
        {
            _retriever = retriever;
            _sender = sender;
            _logger = logger;
        }

        public async Task Handle(OrderStatusChangedToPaidIntegrationEvent @event)
        {
            var subscriptions = await _retriever.GetSubscriptionsOfType(WebhookType.OrderPaid);
            _logger.LogInformation(
                "Received OrderStatusChangedToShippedIntegrationEvent and got {SubscriptionsCount} subscriptions to process",
                subscriptions.Count());
            var whook = new WebhookData(WebhookType.OrderPaid, @event);
            await _sender.SendAll(subscriptions, whook);
        }
    }
}
