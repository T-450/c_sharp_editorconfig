﻿namespace Microsoft.eShopOnContainers.Web.Shopping.HttpAggregator.Models
{
    public class AddBasketItemRequest
    {
        public AddBasketItemRequest()
        {
            Quantity = 1;
        }

        public int CatalogItemId { get; set; }

        public string BasketId { get; set; }

        public int Quantity { get; set; }
    }
}
