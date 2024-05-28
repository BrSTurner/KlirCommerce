using FluentValidation;
using Klir.TechChallenge.Application.ShoppingCart.Commands;

namespace Klir.TechChallenge.Application.ShoppingCart.Validations
{
    public class DeleteCartItemsValidation : AbstractValidator<DeleteCartItemsCommand>
    {
        public DeleteCartItemsValidation()
        {
            RuleFor(x => x.CartId)
                .NotEmpty().WithMessage("Cart Identifier is required");
        }
    }
}
