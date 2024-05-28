using Klir.TechChallenge.Application.ShoppingCart.Models;
using Klir.TechChallenge.Application.ShoppingCart.Validations;
using Klir.TechChallenge.Core.Messages;

namespace Klir.TechChallenge.Application.ShoppingCart.Commands
{
    public class UpdateCartCommand : Command<CartDTO>
    {
        public Guid Id { get; set; }
        public IEnumerable<CartItemDTO> Items { get; set; }

        public override bool IsValid()
        {
            ValidationResult = new UpdateCartValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
