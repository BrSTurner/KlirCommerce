using FluentValidation;
using Klir.TechChallenge.Application.ShoppingCart.Commands;

namespace Klir.TechChallenge.Application.ShoppingCart.Validations
{
    public class UpdateCartValidation : AbstractValidator<UpdateCartCommand>
    {
        public UpdateCartValidation()
        {
            RuleFor(c => c.Id)
                .NotEmpty()
                .WithMessage("Cart Identifier is required");

            RuleFor(x => x.Items)
                .NotEmpty().WithMessage("Cart must contain at least one item");

            RuleForEach(c => c.Items).SetValidator(new UpdateCartItemValidation());
        }
    }
}
