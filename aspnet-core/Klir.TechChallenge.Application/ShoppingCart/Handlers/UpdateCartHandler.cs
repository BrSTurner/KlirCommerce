using Klir.TechChallenge.Application.ShoppingCart.Commands;
using Klir.TechChallenge.Application.ShoppingCart.Models;
using Klir.TechChallenge.Core.Data;
using Klir.TechChallenge.Core.Handlers;
using Klir.TechChallenge.Core.Mediator;
using Klir.TechChallenge.Core.Messages;
using Klir.TechChallenge.Domain.Products.Repositories;
using Klir.TechChallenge.Domain.ShoppingCart.Models;
using Klir.TechChallenge.Domain.ShoppingCart.Repositories;
using MediatR;

namespace Klir.TechChallenge.Application.ShoppingCart.Handlers
{
    public class UpdateCartHandler : CommandHandler, IRequestHandler<UpdateCartCommand, CommandResult<CartDTO>>
    {
        private readonly IProductRepository _productRepository;
        private readonly ICartRepository _cartRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateCartHandler(
            IMediatorHandler mediatorHandler,
            ICartRepository cartRepository,
            IProductRepository productRepository,
            IUnitOfWork unitOfWork) : base(mediatorHandler)
        {
            _productRepository = productRepository;
            _cartRepository = cartRepository;
            _unitOfWork = unitOfWork;
        }


        public async Task<CommandResult<CartDTO>> Handle(UpdateCartCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                await NotifyDomainErrors(request.ValidationResult);
                return CommandResult<CartDTO>.Fail();
            }

            var cart = await _cartRepository.GetAsync(request.Id);

            if (cart is null)
            {
                await AddError("Shopping Cart was not found");
                return CommandResult<CartDTO>.Fail();
            }

            var cartItems = request.Items;

            foreach (var item in cartItems)
            {
                var product = await _productRepository.GetAsync(item.ProductId);

                if (product is null || product.IsInactive)
                {
                    await AddError("One or more products in your Shopping Cart is no longer available");
                    return CommandResult<CartDTO>.Fail();
                }

                var currentItem = cart.Items.FirstOrDefault(i => i.ProductId.Equals(item.ProductId));

                if (currentItem is null)
                {
                    cart.AddItem(new CartItem
                    {
                        Id = Guid.Empty,
                        CartId = cart.Id,
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                        UnitPrice = product.Price,
                        IsPromotionApplied = false,
                        TotalPrice = 0,
                    });

                    continue;
                }

                currentItem.UpdateUnitPrice(product.Price);
                currentItem.UpdateQuantity(item.Quantity);
                currentItem.SetTotalPrice(0);
            }

            _cartRepository.Update(cart);   

            if (!(await CommitChanges(_unitOfWork)).IsValid)
                return CommandResult<CartDTO>.Fail();

            return CommandResult<CartDTO>.Ok(CartDTO.ToCartDTO(cart));
        }
    }
}
