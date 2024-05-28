using Klir.TechChallenge.Application.ShoppingCart.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;

namespace Klir.TechChallenge.Tests.ShoppingCart.Integration
{
    public class CalculateCartIntegrationTest : IntegrationTestBase
    {
        public CalculateCartIntegrationTest(WebApplicationFactory<Program> factory) : base(factory)
        {
        }

        [Theory(DisplayName = "Calculate Carts Success")]
        [InlineData("03f7471f-d4a9-4ada-8a40-7c0cea29fa03", 48, true, 2)]
        [InlineData("5e62b18c-63f3-447d-915a-1ece820c19f7", 30, true, 2)]
        public async Task Calculate_Cart_Success(string id, decimal expectedTotal, bool isPromotionApplied, int promotionsAmount)
        {
            // Arrange
            var request = new HttpRequestMessage(HttpMethod.Get, $"/api/cart/{id}/calculated"); 

            // Act
            var response = await Client.SendAsync(request);

            // Assert
            response.EnsureSuccessStatusCode(); 
            var content = await response.Content.ReadFromJsonAsync<IntegrationResponse<CartDTO>>();

            Assert.True(content.Success);
            Assert.Equal(expectedTotal, content.Data.Total);
            Assert.Equal(isPromotionApplied, content.Data.Items.Any(x => x.IsPromotionApplied));
            Assert.Equal(promotionsAmount, content.Data.Items.Where(x => x.IsPromotionApplied).Count());
        }
    }
}
