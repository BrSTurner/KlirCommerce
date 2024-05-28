using Klir.TechChallenge.Domain.Products.Models;
using Klir.TechChallenge.Domain.Promotions.Models;
using Klir.TechChallenge.Domain.ShoppingCart.Models;
using Klir.TechChallenge.Infra.Data;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Klir.TechChallenge.Tests.ShoppingCart.Integration
{
    public abstract class IntegrationTestBase : IClassFixture<WebApplicationFactory<Program>>, IDisposable
    {
        protected readonly HttpClient Client;

        public IntegrationTestBase(WebApplicationFactory<Program> factory) 
        {
            Client = factory.WithWebHostBuilder(builder => builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<KlirCommerceContext>));

                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }


                services.AddDbContext<KlirCommerceContext>(options =>
                {
                    options.UseInMemoryDatabase("KlirCommerce");
                });


                var serviceProvider = services.BuildServiceProvider();


                using (var scope = serviceProvider.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var db = scopedServices.GetRequiredService<KlirCommerceContext>();
                    var logger = scopedServices.GetRequiredService<ILogger<IntegrationTestBase>>();


                    db.Database.EnsureCreated();

                    try
                    {
                        SeedDatabase(db);
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, "An error occurred seeding the database with test messages. Error: {Message}", ex.Message);
                    }
                }

            })).CreateClient();
        }

        public void Dispose()
        {
            Client.Dispose();
        }

        private void SeedDatabase(KlirCommerceContext context)
        {
            context.Set<Promotion>().AddRange(MockPromotionTable.GetInstance().Promotions);
            context.Set<Product>().AddRange(MockProductTable.GetInstance().Products);

            var carts = MockCartTable.GetInstance().Carts.Select(x => new Cart
            {
                Id = x.Id,
                Items = x.Items.Select(i => new CartItem
                {
                    CartId = i.Id,
                    IsPromotionApplied = false,
                    ProductId = i.ProductId,
                    Quantity = i.Quantity,
                    TotalPrice = 0,
                    UnitPrice = i.UnitPrice,
                }).ToList(),
                Total = 0
            });

            context.Set<Cart>().AddRange(carts);

            context.SaveChanges();
        }
    }
}
