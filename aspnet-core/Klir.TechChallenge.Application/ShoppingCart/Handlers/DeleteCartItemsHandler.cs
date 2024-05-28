using Klir.TechChallenge.Application.ShoppingCart.Commands;
using Klir.TechChallenge.Core.Data;
using Klir.TechChallenge.Core.Handlers;
using Klir.TechChallenge.Core.Mediator;
using Klir.TechChallenge.Core.Messages;
using Klir.TechChallenge.Domain.ShoppingCart.Repositories;
using MediatR;

namespace Klir.TechChallenge.Application.ShoppingCart.Handlers
{
    public class DeleteCartItemsHandler : CommandHandler, IRequestHandler<DeleteCartItemsCommand, CommandResult<bool>>
    {
        private readonly ICartRepository _cartRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCartItemsHandler(
            IMediatorHandler mediatorHandler,
            ICartRepository cartRepository,
            IUnitOfWork unitOfWork) : base(mediatorHandler)
        {
            _cartRepository = cartRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<CommandResult<bool>> Handle(DeleteCartItemsCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                await NotifyDomainErrors(request.ValidationResult);
                return CommandResult<bool>.Fail();
            }

            var cart = await _cartRepository.GetAsync(request.CartId);

            if (cart is null)
            {
                await AddError("We could not find your Shopping Cart, try again later");
                return CommandResult<bool>.Fail();
            }

            cart.Clear();

            _cartRepository.Update(cart);

            if (!(await CommitChanges(_unitOfWork)).IsValid)
                return CommandResult<bool>.Fail();

            return CommandResult<bool>.Ok(true);
        }
    }
}
