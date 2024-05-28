using Klir.TechChallenge.Application.ShoppingCart.Models;
using Klir.TechChallenge.Application.ShoppingCart.Validations;
using Klir.TechChallenge.Core.Messages;

namespace Klir.TechChallenge.Application.ShoppingCart.Commands
{
    public class CreateCartCommand : Command<Guid>
    {
        public IEnumerable<CartItemDTO> Items { get; set; } = Enumerable.Empty<CartItemDTO>();

        public override bool IsValid()
        {
            ValidationResult = new CreateCartValidation().Validate(this);
            return ValidationResult.IsValid;    
        }
    }
}
