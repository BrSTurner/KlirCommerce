using Klir.TechChallenge.Application.ShoppingCart.Models;
using Klir.TechChallenge.Application.ShoppingCart.Validations;
using Klir.TechChallenge.Core.Messages;

namespace Klir.TechChallenge.Application.ShoppingCart.Commands
{
    public class CalculateCartCommand : Command<CartDTO>
    {
        public Guid CartId { get; set; }

        public override bool IsValid()
        {
            ValidationResult = new CalculateCartValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
