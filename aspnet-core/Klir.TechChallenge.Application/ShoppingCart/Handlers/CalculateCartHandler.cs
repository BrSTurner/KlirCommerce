using Klir.TechChallenge.Application.ShoppingCart.Commands;
using Klir.TechChallenge.Application.ShoppingCart.Models;
using Klir.TechChallenge.Core.Handlers;
using Klir.TechChallenge.Core.Mediator;
using Klir.TechChallenge.Core.Messages;
using Klir.TechChallenge.Domain.ShoppingCart.Repositories;
using Klir.TechChallenge.Domain.ShoppingCart.Services;
using MediatR;

namespace Klir.TechChallenge.Application.ShoppingCart.Handlers
{
    public class CalculateCartHandler : CommandHandler,
        IRequestHandler<CalculateCartCommand, CommandResult<CartDTO>>
    {
        private readonly ICartRepository _cartRepository;
        private readonly ICartService _cartService;

        public CalculateCartHandler(
            IMediatorHandler mediatorHandler,
            ICartRepository cartRepository,
            ICartService cartService) : base(mediatorHandler)
        {
            _cartRepository = cartRepository;
            _cartService = cartService;
        }

        public async Task<CommandResult<CartDTO>> Handle(CalculateCartCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                await NotifyDomainErrors(request.ValidationResult);
                return CommandResult<CartDTO>.Fail();
            }

            var currentCart = await _cartRepository.GetAsync(request.CartId);

            if (currentCart is null)
            {
                await AddError("Could not find a valid Shopping Cart");
                return CommandResult<CartDTO>.Fail();
            }

            var calculationResult = await _cartService.CalculateShoppingCart(request.CartId);

            if (calculationResult.IsFailure)
            {
                await AddError(calculationResult.Error);
                return CommandResult<CartDTO>.Fail();
            }

            return CommandResult<CartDTO>.Ok(CartDTO.ToCartDTO(calculationResult.Value));
        }
    }
}
