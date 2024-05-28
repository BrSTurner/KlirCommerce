using Klir.TechChallenge.Application.ShoppingCart.Validations;
using Klir.TechChallenge.Core.Messages;

namespace Klir.TechChallenge.Application.ShoppingCart.Commands
{
    public class DeleteCartItemsCommand : Command<bool>
    {
        public Guid CartId { get; set; }

        public override bool IsValid()
        {
            ValidationResult = new DeleteCartItemsValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
