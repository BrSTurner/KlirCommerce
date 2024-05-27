using Klir.TechChallenge.Domain.Promotions.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Klir.TechChallenge.Infra.Promotions.Mapping
{
    public class PromotionMapping : IEntityTypeConfiguration<Promotion>
    {
        public void Configure(EntityTypeBuilder<Promotion> builder)
        {
            builder.ToTable("Promotions");

            builder.HasKey(x => x.Id);

            builder.Property(p => p.Name).IsRequired();
            builder.Property(p => p.Description).IsRequired();
            builder.Property(p => p.RequiredQuantity).IsRequired();
            builder.Property(p => p.Type).IsRequired();
            builder.Property(p => p.IsActive)
                .IsRequired()
                .HasDefaultValue(true);

            SeedData(builder);
        }

        private void SeedData(EntityTypeBuilder<Promotion> builder)
        {
            builder.HasData(new List<Promotion>
            {
                {
                    new Promotion
                    {
                        Id = Guid.Parse("fa8c3e23-7e44-4f53-aebc-7c5f3be2e197"),
                        Name = "Buy 1 Get 1 Free",
                        Description = "Get 2 for the price of 1.",
                        Type = PromotionType.BuyOneGetOne,
                        RequiredQuantity = 2,
                        IsActive = true
                    }
                },
                {
                    new Promotion
                    {
                        Id = Guid.Parse("d6a5a9a1-45b9-4b91-9a7e-3c6f0e34e935"),
                        Name = "3 for 10 Euro",
                        Description = "Buy 3 products for only 10 Euro.",
                        Type = PromotionType.ThreeForTen,
                        RequiredQuantity = 3,
                        IsActive = true
                    }
                }
            });
        }
    }
}
