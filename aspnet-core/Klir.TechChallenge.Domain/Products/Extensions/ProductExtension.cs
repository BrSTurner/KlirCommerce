using Klir.TechChallenge.Domain.Products.Models;

namespace Klir.TechChallenge.Domain.Products.Extensions
{
    public static class ProductExtension
    {
        public static bool AnyInactive(this IEnumerable<Product> products) => products.Any(x => !x.IsActive);
    }
}
