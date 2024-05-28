using FluentValidation;
using Klir.TechChallenge.Application.ShoppingCart.Commands;

namespace Klir.TechChallenge.Application.ShoppingCart.Validations
{
    public class CalculateCartValidation : AbstractValidator<CalculateCartCommand>
    {
        public CalculateCartValidation()
        {
            RuleFor(x => x.CartId)
                .NotEmpty().WithMessage("Cart Identifier is required");
        }
    }
}
