using FluentValidation;
using Klir.TechChallenge.Application.ShoppingCart.Commands;

namespace Klir.TechChallenge.Application.ShoppingCart.Validations
{
    public class CreateCartValidation : AbstractValidator<CreateCartCommand>
    {
        public CreateCartValidation()
        {
            RuleFor(x => x.Items)
                .NotEmpty().WithMessage("Cart must contain at least one item");

            RuleForEach(x => x.Items).SetValidator(new CreateCartItemValidation());
        }
    }
}
