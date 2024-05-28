using Klir.TechChallenge.Core.Data;
using Klir.TechChallenge.Domain.Promotions.Models;
using Klir.TechChallenge.Domain.ShoppingCart.Models;
using Klir.TechChallenge.Domain.ShoppingCart.PriceCalculationStrategy;
using Klir.TechChallenge.Domain.ShoppingCart.PriceCalculationStrategy.Abstraction;
using Klir.TechChallenge.Domain.ShoppingCart.Repositories;
using Klir.TechChallenge.Domain.ShoppingCart.Services;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace Klir.TechChallenge.Tests.ShoppingCart.Domain
{
    public class CartServiceUnitTest
    {
        [Fact(DisplayName = "Calculate Empty Error Message")]
        public async Task Calculate_EmptyCartId_ErrorMessage()
        {
            //Arrange
            var priceCalculatorResolver = new Mock<IPriceCalculatorStrategyResolver>();
            var cartRepositoryMock = new Mock<ICartRepository>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();

            cartRepositoryMock
                .Setup(x => x.GetAsync(It.Is<Guid>(x => x.Equals(Guid.Empty))))
                .ReturnsAsync(default(Cart));

            var cartService = new CartService(priceCalculatorResolver.Object,
                                              cartRepositoryMock.Object,
                                              unitOfWorkMock.Object);

            var cartId = Guid.Empty;

            //Act
            var result = await cartService.CalculateShoppingCart(cartId);

            //Assert
            Assert.NotNull(result);
            Assert.True(result.IsFailure);
            Assert.False(result.IsSuccess);
            Assert.NotEmpty(result.Error);
            Assert.Equal("We could not find your Shopping Cart, try again later.", result.Error);
        }


        [Theory(DisplayName = "Calculate Carts Success")]
        [InlineData("03f7471f-d4a9-4ada-8a40-7c0cea29fa03", 48, true,  2)]
        [InlineData("7c257c4a-5b53-4971-99d4-6c3d41ddda47", 00, false, 0)]
        [InlineData("5e62b18c-63f3-447d-915a-1ece820c19f7", 30, true,  2)]
        public async Task Calculate_Carts_Success(string id, decimal expectedTotal, bool isPromotionApplied, int promotionsAmount)
        {
            //Arrange
            var cartId = Guid.Parse(id);
            var priceCalculatorResolver = new Mock<IPriceCalculatorStrategyResolver>();
            var cartRepositoryMock = new Mock<ICartRepository>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var cart = MockCartTable.GetInstance().GetCart(cartId);

            cartRepositoryMock
                .Setup(x => x.GetAsync(It.Is<Guid>(id => id.Equals(cartId))))
                .ReturnsAsync(cart);

            unitOfWorkMock
                .Setup(x => x.CommitAsync())
                .ReturnsAsync(true);

            priceCalculatorResolver
                .Setup(x => x.GetPriceCalculator(It.Is<PromotionType>(type => type.Equals(PromotionType.None))))
                .Returns(new RegularPriceCalculatorStrategy());

            priceCalculatorResolver
                .Setup(x => x.GetPriceCalculator(It.Is<PromotionType>(type => type.Equals(PromotionType.BuyOneGetOne))))
                .Returns(new BuyOneGetOnePriceCalculatorStrategy());

            priceCalculatorResolver
                .Setup(x => x.GetPriceCalculator(It.Is<PromotionType>(type => type.Equals(PromotionType.ThreeForTen))))
                .Returns(new ThreeForTenPriceCalculatorStrategy());


            var cartService = new CartService(priceCalculatorResolver.Object,
                                              cartRepositoryMock.Object,
                                              unitOfWorkMock.Object);

 
            //Act
            var result = await cartService.CalculateShoppingCart(cartId);

            //Assert
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
            Assert.False(result.IsFailure);
            Assert.Empty(result.Error);
            Assert.Equal(expectedTotal, result.Value.Total);
            Assert.Equal(isPromotionApplied, result.Value.Items.Any(x => x.IsPromotionApplied));
            Assert.Equal(promotionsAmount, result.Value.Items.Where(x => x.IsPromotionApplied).Count());
        }
    }
}
