using Klir.TechChallenge.Domain.ShoppingCart.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Klir.TechChallenge.Infra.ShoppingCart.Mapping
{
    public class CartMapping : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.ToTable("ShoppingCart");

            builder.HasKey(x => x.Id);

            builder.Property(p => p.Total).HasColumnType("decimal(18,2)").IsRequired();

            builder.HasMany(c => c.Items)
                .WithOne(i => i.Cart)
                .HasPrincipalKey(c => c.Id)
                .HasForeignKey(i => i.CartId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
