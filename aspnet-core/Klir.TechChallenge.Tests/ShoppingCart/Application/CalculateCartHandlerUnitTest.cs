using Klir.TechChallenge.Application.ShoppingCart.Commands;
using Klir.TechChallenge.Application.ShoppingCart.Handlers;
using Klir.TechChallenge.Core.Mediator;
using Klir.TechChallenge.Domain.Models;
using Klir.TechChallenge.Domain.ShoppingCart.Models;
using Klir.TechChallenge.Domain.ShoppingCart.Repositories;
using Klir.TechChallenge.Domain.ShoppingCart.Services;
using Moq;

namespace Klir.TechChallenge.Tests.ShoppingCart.Application
{
    public class CalculateCartHandlerUnitTest
    {
        [Fact(DisplayName = "Calculate Invalid Command Fail")]
        public async Task Handle_InvalidCommand_ReturnsFail()
        {
            // Arrange
            var mediatorMock = new Mock<IMediatorHandler>();
            var cartRepositoryMock = new Mock<ICartRepository>();
            var cartServiceMock = new Mock<ICartService>();

            var handler = new CalculateCartHandler(mediatorMock.Object, cartRepositoryMock.Object, cartServiceMock.Object);
            var command = new CalculateCartCommand();

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.Success);
        }

        [Fact(DisplayName = "Calculate Inexistent Cart Fail")]
        public async Task Handle_InexistentCart_ReturnsFail()
        {
            // Arrange
            var mediatorMock = new Mock<IMediatorHandler>();
            var cartRepositoryMock = new Mock<ICartRepository>();
            var cartServiceMock = new Mock<ICartService>();
            var cartId = Guid.NewGuid();

            cartRepositoryMock
                .Setup(x => x.GetAsync(It.Is<Guid>(x => x.Equals(cartId))))
                .ReturnsAsync(default(Cart));

            var handler = new CalculateCartHandler(mediatorMock.Object, cartRepositoryMock.Object, cartServiceMock.Object);
            var command = new CalculateCartCommand{ CartId = cartId };

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.Success);
        }

        [Fact(DisplayName = "Calculate Valid Cart Success")]
        public async Task Handle_ValidCart_ReturnsSuccess()
        {
            // Arrange
            var mediatorMock = new Mock<IMediatorHandler>();
            var cartRepositoryMock = new Mock<ICartRepository>();
            var cartServiceMock = new Mock<ICartService>();
            var cart = MockCartTable.GetInstance().Carts.First();

            cartRepositoryMock
                .Setup(x => x.GetAsync(It.Is<Guid>(x => x.Equals(cart.Id))))
                .ReturnsAsync(cart);

            cartServiceMock
                .Setup(x => x.CalculateShoppingCart(It.Is<Guid>(x => x.Equals(cart.Id))))
                .ReturnsAsync(Result<Cart>.Ok(cart));

            var handler = new CalculateCartHandler(mediatorMock.Object, cartRepositoryMock.Object, cartServiceMock.Object);
            var command = new CalculateCartCommand { CartId = cart.Id };

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Data);
            Assert.True(result.Success);
        }
    }
}

