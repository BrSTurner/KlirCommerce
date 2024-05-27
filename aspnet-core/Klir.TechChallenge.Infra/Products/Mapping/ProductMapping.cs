using Klir.TechChallenge.Domain.Products.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Klir.TechChallenge.Infra.Products.Mapping
{
    public class ProductMapping : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");

            builder.HasKey(x => x.Id);

            builder.Property(p => p.Name).IsRequired();
            builder.Property(p => p.Price).HasColumnType("decimal(18,2)").IsRequired();

            builder.Property(p => p.IsActive)
                .IsRequired()
                .HasDefaultValue(true);

            builder.HasOne(x => x.Promotion)
                .WithMany(x => x.Products)
                .HasPrincipalKey(x => x.Id)
                .HasForeignKey(x => x.PromotionId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Ignore(p => p.IsInactive);

            SeedData(builder);
        }

        private void SeedData(EntityTypeBuilder<Product> builder)
        {
            builder.HasData(new List<Product>()
            {
                {
                    new Product
                    {
                        Name = "Product A",
                        Price = 20.00m,
                        PromotionId = Guid.Parse("fa8c3e23-7e44-4f53-aebc-7c5f3be2e197"),
                        IsActive = true,
                    }
                },
                {
                    new Product
                    {
                        Name = "Product B",
                        Price = 4.00m,
                        PromotionId = Guid.Parse("d6a5a9a1-45b9-4b91-9a7e-3c6f0e34e935"),
                        IsActive = true,
                    }
                },
                {
                    new Product
                    {
                        Name = "Product C",
                        Price = 2.00m,
                        IsActive = true,
                    }
                },
                {
                    new Product
                    {
                        Name = "Product D",
                        Price = 4.00m,
                        PromotionId = Guid.Parse("d6a5a9a1-45b9-4b91-9a7e-3c6f0e34e935"),
                        IsActive = true,
                    }
                }
            });
        }
    }
}
