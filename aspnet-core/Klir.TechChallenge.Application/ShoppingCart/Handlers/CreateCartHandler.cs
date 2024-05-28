using Klir.TechChallenge.Application.ShoppingCart.Commands;
using Klir.TechChallenge.Core.Data;
using Klir.TechChallenge.Core.Handlers;
using Klir.TechChallenge.Core.Mediator;
using Klir.TechChallenge.Core.Messages;
using Klir.TechChallenge.Domain.Products.Extensions;
using Klir.TechChallenge.Domain.Products.Repositories;
using Klir.TechChallenge.Domain.ShoppingCart.Models;
using Klir.TechChallenge.Domain.ShoppingCart.Repositories;
using MediatR;

namespace Klir.TechChallenge.Application.ShoppingCart.Handlers
{
    public class CreateCartHandler : CommandHandler, IRequestHandler<CreateCartCommand, CommandResult<Guid>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductRepository _productRepository;
        private readonly ICartRepository _cartRepository;

        public CreateCartHandler(
            IMediatorHandler mediatorHandler,
            IUnitOfWork unitOfWork,
            IProductRepository productRepository,
            ICartRepository cartRepository) : base(mediatorHandler)
        {
            _unitOfWork = unitOfWork;
            _productRepository = productRepository;
            _cartRepository = cartRepository;
        }

        public async Task<CommandResult<Guid>> Handle(CreateCartCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                await NotifyDomainErrors(request.ValidationResult);
                return CommandResult<Guid>.Fail();
            }

            var products = await _productRepository.GetMultipleByIdNoTrackingAsync(request.Items.Select(x => x.ProductId).ToList());

            if (!products.Any() || products.Count() != request.Items.Count() || products.AnyInactive())
            {
                await AddError("One or more products are no longer available");
                return CommandResult<Guid>.Fail();
            }

            var cart = new Cart();

            cart.Items = request.Items.Select(i => new CartItem
            {
                CartId = cart.Id,
                ProductId = i.ProductId,
                Quantity = i.Quantity,
                UnitPrice = i.UnitPrice,
            }).ToList();

            await _cartRepository.AddAsync(cart);

            if (!(await CommitChanges(_unitOfWork)).IsValid)
            {
                return CommandResult<Guid>.Fail();
            }

            return CommandResult<Guid>.Ok(cart.Id);
        }
    }
}
