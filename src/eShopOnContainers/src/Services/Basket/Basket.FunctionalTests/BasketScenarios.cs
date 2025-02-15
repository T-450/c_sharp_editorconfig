﻿namespace Basket.FunctionalTests
{
    using API.Model;

    public class BasketScenarios
        : BasketScenarioBase
    {
        [Fact]
        public async Task Post_basket_and_response_ok_status_code()
        {
            using var server = CreateServer();
            var content = new StringContent(BuildBasket(), Encoding.UTF8, "application/json");
            var response = await server.CreateClient()
                .PostAsync(Post.Basket, content);

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task Get_basket_and_response_ok_status_code()
        {
            using var server = CreateServer();
            var response = await server.CreateClient()
                .GetAsync(Get.GetBasket(1));

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task Send_Checkout_basket_and_response_ok_status_code()
        {
            using var server = CreateServer();
            var contentBasket = new StringContent(BuildBasket(), Encoding.UTF8, "application/json");

            await server.CreateClient()
                .PostAsync(Post.Basket, contentBasket);

            var contentCheckout = new StringContent(BuildCheckout(), Encoding.UTF8, "application/json");

            var response = await server.CreateIdempotentClient()
                .PostAsync(Post.CheckoutOrder, contentCheckout);

            response.EnsureSuccessStatusCode();
        }

        private string BuildBasket()
        {
            var order = new CustomerBasket(AutoAuthorizeMiddleware.IDENTITY_ID);

            order.Items.Add(new BasketItem
            {
                ProductId = 1,
                ProductName = ".NET Bot Black Hoodie",
                UnitPrice = 10,
                Quantity = 1,
            });

            return JsonSerializer.Serialize(order);
        }

        private string BuildCheckout()
        {
            var checkoutBasket = new
            {
                City = "city",
                Street = "street",
                State = "state",
                Country = "coutry",
                ZipCode = "zipcode",
                CardNumber = "1234567890123456",
                CardHolderName = "CardHolderName",
                CardExpiration = DateTime.UtcNow.AddDays(1),
                CardSecurityNumber = "123",
                CardTypeId = 1,
                Buyer = "Buyer",
                RequestId = Guid.NewGuid(),
            };

            return JsonSerializer.Serialize(checkoutBasket);
        }
    }
}
