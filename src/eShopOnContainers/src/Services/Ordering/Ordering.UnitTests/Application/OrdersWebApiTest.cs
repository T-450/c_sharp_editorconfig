﻿namespace UnitTest.Ordering.Application
{
    using System.Net;
    using CardType = Microsoft.eShopOnContainers.Services.Ordering.API.Application.Queries.CardType;
    using Order = Microsoft.eShopOnContainers.Services.Ordering.API.Application.Queries.Order;

    public class OrdersWebApiTest
    {
        private readonly Mock<IIdentityService> _identityServiceMock;
        private readonly Mock<ILogger<OrdersController>> _loggerMock;
        private readonly Mock<IMediator> _mediatorMock;
        private readonly Mock<IOrderQueries> _orderQueriesMock;

        public OrdersWebApiTest()
        {
            _mediatorMock = new Mock<IMediator>();
            _orderQueriesMock = new Mock<IOrderQueries>();
            _identityServiceMock = new Mock<IIdentityService>();
            _loggerMock = new Mock<ILogger<OrdersController>>();
        }

        [Fact]
        public async Task Cancel_order_with_requestId_success()
        {
            //Arrange
            _mediatorMock.Setup(x => x.Send(It.IsAny<IdentifiedCommand<CancelOrderCommand, bool>>(), default))
                .Returns(Task.FromResult(true));

            //Act
            var orderController = new OrdersController(_mediatorMock.Object, _orderQueriesMock.Object, _identityServiceMock.Object,
                _loggerMock.Object);
            var actionResult = await orderController.CancelOrderAsync(new CancelOrderCommand(1), Guid.NewGuid().ToString()) as OkResult;

            //Assert
            Assert.Equal(actionResult.StatusCode, (int)HttpStatusCode.OK);

        }

        [Fact]
        public async Task Cancel_order_bad_request()
        {
            //Arrange
            _mediatorMock.Setup(x => x.Send(It.IsAny<IdentifiedCommand<CancelOrderCommand, bool>>(), default))
                .Returns(Task.FromResult(true));

            //Act
            var orderController = new OrdersController(_mediatorMock.Object, _orderQueriesMock.Object, _identityServiceMock.Object,
                _loggerMock.Object);
            var actionResult = await orderController.CancelOrderAsync(new CancelOrderCommand(1), string.Empty) as BadRequestResult;

            //Assert
            Assert.Equal(actionResult.StatusCode, (int)HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Ship_order_with_requestId_success()
        {
            //Arrange
            _mediatorMock.Setup(x => x.Send(It.IsAny<IdentifiedCommand<ShipOrderCommand, bool>>(), default))
                .Returns(Task.FromResult(true));

            //Act
            var orderController = new OrdersController(_mediatorMock.Object, _orderQueriesMock.Object, _identityServiceMock.Object,
                _loggerMock.Object);
            var actionResult = await orderController.ShipOrderAsync(new ShipOrderCommand(1), Guid.NewGuid().ToString()) as OkResult;

            //Assert
            Assert.Equal(actionResult.StatusCode, (int)HttpStatusCode.OK);

        }

        [Fact]
        public async Task Ship_order_bad_request()
        {
            //Arrange
            _mediatorMock.Setup(x => x.Send(It.IsAny<IdentifiedCommand<CreateOrderCommand, bool>>(), default))
                .Returns(Task.FromResult(true));

            //Act
            var orderController = new OrdersController(_mediatorMock.Object, _orderQueriesMock.Object, _identityServiceMock.Object,
                _loggerMock.Object);
            var actionResult = await orderController.ShipOrderAsync(new ShipOrderCommand(1), string.Empty) as BadRequestResult;

            //Assert
            Assert.Equal(actionResult.StatusCode, (int)HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Get_orders_success()
        {
            //Arrange
            var fakeDynamicResult = Enumerable.Empty<OrderSummary>();

            _identityServiceMock.Setup(x => x.GetUserIdentity())
                .Returns(Guid.NewGuid().ToString());

            _orderQueriesMock.Setup(x => x.GetOrdersFromUserAsync(Guid.NewGuid()))
                .Returns(Task.FromResult(fakeDynamicResult));

            //Act
            var orderController = new OrdersController(_mediatorMock.Object, _orderQueriesMock.Object, _identityServiceMock.Object,
                _loggerMock.Object);
            var actionResult = await orderController.GetOrdersAsync();

            //Assert
            Assert.Equal((actionResult.Result as OkObjectResult).StatusCode, (int)HttpStatusCode.OK);
        }

        [Fact]
        public async Task Get_order_success()
        {
            //Arrange
            var fakeOrderId = 123;
            var fakeDynamicResult = new Order();
            _orderQueriesMock.Setup(x => x.GetOrderAsync(It.IsAny<int>()))
                .Returns(Task.FromResult(fakeDynamicResult));

            //Act
            var orderController = new OrdersController(_mediatorMock.Object, _orderQueriesMock.Object, _identityServiceMock.Object,
                _loggerMock.Object);
            var actionResult = await orderController.GetOrderAsync(fakeOrderId) as OkObjectResult;

            //Assert
            Assert.Equal(actionResult.StatusCode, (int)HttpStatusCode.OK);
        }

        [Fact]
        public async Task Get_cardTypes_success()
        {
            //Arrange
            var fakeDynamicResult = Enumerable.Empty<CardType>();
            _orderQueriesMock.Setup(x => x.GetCardTypesAsync())
                .Returns(Task.FromResult(fakeDynamicResult));

            //Act
            var orderController = new OrdersController(_mediatorMock.Object, _orderQueriesMock.Object, _identityServiceMock.Object,
                _loggerMock.Object);
            var actionResult = await orderController.GetCardTypesAsync();

            //Assert
            Assert.Equal((actionResult.Result as OkObjectResult).StatusCode, (int)HttpStatusCode.OK);
        }
    }
}
