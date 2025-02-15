﻿namespace UnitTest.Ordering.Application
{
    using Microsoft.eShopOnContainers.Services.Ordering.API.Application.IntegrationEvents;
    using Order = Microsoft.eShopOnContainers.Services.Ordering.Domain.AggregatesModel.OrderAggregate.Order;

    public class NewOrderRequestHandlerTest
    {
        private readonly Mock<IIdentityService> _identityServiceMock;
        private readonly Mock<IMediator> _mediator;
        private readonly Mock<IOrderingIntegrationEventService> _orderingIntegrationEventService;
        private readonly Mock<IOrderRepository> _orderRepositoryMock;

        public NewOrderRequestHandlerTest()
        {

            _orderRepositoryMock = new Mock<IOrderRepository>();
            _identityServiceMock = new Mock<IIdentityService>();
            _orderingIntegrationEventService = new Mock<IOrderingIntegrationEventService>();
            _mediator = new Mock<IMediator>();
        }

        [Fact]
        public async Task Handle_return_false_if_order_is_not_persisted()
        {
            var buyerId = "1234";

            var fakeOrderCmd = FakeOrderRequestWithBuyer(new Dictionary<string, object>
                { ["cardExpiration"] = DateTime.Now.AddYears(1) });

            _orderRepositoryMock.Setup(orderRepo => orderRepo.GetAsync(It.IsAny<int>()))
                .Returns(Task.FromResult(FakeOrder()));

            _orderRepositoryMock.Setup(buyerRepo => buyerRepo.UnitOfWork.SaveChangesAsync(default))
                .Returns(Task.FromResult(1));

            _identityServiceMock.Setup(svc => svc.GetUserIdentity()).Returns(buyerId);

            var LoggerMock = new Mock<ILogger<CreateOrderCommandHandler>>();
            //Act
            var handler = new CreateOrderCommandHandler(_mediator.Object, _orderingIntegrationEventService.Object,
                _orderRepositoryMock.Object, _identityServiceMock.Object, LoggerMock.Object);
            var cltToken = new CancellationToken();
            bool result = await handler.Handle(fakeOrderCmd, cltToken);

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void Handle_throws_exception_when_no_buyerId()
        {
            //Assert
            Assert.Throws<ArgumentNullException>(() => new Buyer(string.Empty, string.Empty));
        }

        private Buyer FakeBuyer()
        {
            return new Buyer(Guid.NewGuid().ToString(), "1");
        }

        private Order FakeOrder()
        {
            return new Order("1", "fakeName", new Address("street", "city", "state", "country", "zipcode"), 1, "12", "111", "fakeName",
                DateTime.Now.AddYears(1));
        }

        private CreateOrderCommand FakeOrderRequestWithBuyer(Dictionary<string, object> args = null)
        {
            return new CreateOrderCommand(
                new List<BasketItem>(),
                args != null && args.ContainsKey("userId") ? (string)args["userId"] : null,
                args != null && args.ContainsKey("userName") ? (string)args["userName"] : null,
                args != null && args.ContainsKey("city") ? (string)args["city"] : null,
                args != null && args.ContainsKey("street") ? (string)args["street"] : null,
                args != null && args.ContainsKey("state") ? (string)args["state"] : null,
                args != null && args.ContainsKey("country") ? (string)args["country"] : null,
                args != null && args.ContainsKey("zipcode") ? (string)args["zipcode"] : null,
                args != null && args.ContainsKey("cardNumber") ? (string)args["cardNumber"] : "1234",
                cardExpiration: args != null && args.ContainsKey("cardExpiration") ? (DateTime)args["cardExpiration"] : DateTime.MinValue,
                cardSecurityNumber: args != null && args.ContainsKey("cardSecurityNumber") ? (string)args["cardSecurityNumber"] : "123",
                cardHolderName: args != null && args.ContainsKey("cardHolderName") ? (string)args["cardHolderName"] : "XXX",
                cardTypeId: args != null && args.ContainsKey("cardTypeId") ? (int)args["cardTypeId"] : 0);
        }
    }
}
