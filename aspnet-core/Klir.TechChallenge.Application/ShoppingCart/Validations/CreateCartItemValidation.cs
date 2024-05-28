using FluentValidation;
using Klir.TechChallenge.Application.ShoppingCart.Models;

namespace Klir.TechChallenge.Application.ShoppingCart.Validations
{
    public class CreateCartItemValidation : AbstractValidator<CartItemDTO>
    {
        public CreateCartItemValidation()
        {
            RuleFor(x => x.ProductId)
                .NotEmpty().WithMessage("Product information is required");

            RuleFor(x => x.Quantity)
                .GreaterThan(0).WithMessage("Product quantity must be greater than 0");

            RuleFor(x => x.UnitPrice)
                .GreaterThanOrEqualTo(0).WithMessage("Product price must be positive");
        }
    }
}
