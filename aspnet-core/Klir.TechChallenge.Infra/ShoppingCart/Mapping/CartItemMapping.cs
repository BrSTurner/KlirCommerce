using Klir.TechChallenge.Domain.ShoppingCart.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Klir.TechChallenge.Infra.ShoppingCart.Mapping
{
    public class CartItemMapping : IEntityTypeConfiguration<CartItem>
    {
        public void Configure(EntityTypeBuilder<CartItem> builder)
        {
            builder.ToTable("CartItems");

            builder.HasKey(x => x.Id);

            builder.Property(p => p.Quantity).IsRequired();
            builder.Property(p => p.UnitPrice).HasColumnType("decimal(18,2)").IsRequired();
            builder.Property(p => p.IsPromotionApplied).IsRequired().HasDefaultValue(false);
            builder.Property(p => p.TotalPrice).IsRequired().HasColumnType("decimal(18,2)");

            builder.HasOne(i => i.Product)
                .WithMany(p => p.CartItems)
                .HasForeignKey(i => i.ProductId)
                .HasPrincipalKey(p => p.Id)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
