using AppleShop.cart.commandApplication.Commands.Cart;
using AppleShop.cart.commandApplication.Validator.Cart;
using AppleShop.cart.Domain.Abstractions.IRepositories;
using AppleShop.Share.Events.Cart.Response;
using AppleShop.Share.Exceptions;
using AppleShop.Share.Shared;
using AutoMapper;
using MassTransit;
using MediatR;
using Entities = AppleShop.cart.Domain.Entities;

namespace AppleShop.cart.commandApplication.Handler.Cart
{
    public class CreateCartCommandHandler : IRequestHandler<CreateCartCommand, Result<object>>
    {
        private readonly ICartRepository cartRepository;
        private readonly IMapper mapper;
        private readonly ICartItemRepository cartItemRepository;
        private readonly IRequestClient<GetProductByIdEvent> getProductByIdClient;

        public CreateCartCommandHandler(ICartRepository cartRepository, IMapper mapper, ICartItemRepository cartItemRepository, IRequestClient<GetProductByIdEvent> getProductByIdClient)
        {
            this.cartRepository = cartRepository;
            this.mapper = mapper;
            this.cartItemRepository = cartItemRepository;
            this.getProductByIdClient = getProductByIdClient;
        }

        public async Task<Result<object>> Handle(CreateCartCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateCartCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid) AppleException.ThrowValidation(validationResult);

            using var transaction = await cartRepository.BeginTransactionAsync(cancellationToken);
            try
            {
                var cart = await cartRepository.FindSingleAsync(x => x.UserId == request.UserId, true);
                if (cart is null)
                {
                    cart = new Entities.Cart
                    {
                        UserId = request.UserId,
                        CreatedAt = DateTime.Now
                    };
                    cartRepository.Create(cart);
                    await cartRepository.SaveChangesAsync(cancellationToken);
                }
                var existingCartItems = cartItemRepository.FindAll(x => x.CartId == cart.Id, true).ToDictionary(ci => ci.ProductId, ci => ci);
                var cartTasks = request.CartItems.Select(async item =>
                {
                    if (existingCartItems.TryGetValue(item.ProductId, out var existingCartItem))
                    {
                        existingCartItem.Quantity = item.Quantity;
                        cartItemRepository.Update(existingCartItem);
                    }
                    else
                    {
                        var productRequest = new GetProductByIdEvent { ProductId = item.ProductId };
                        var productResponse = await getProductByIdClient.GetResponse<ProductResponse>(productRequest, cancellationToken);
                        var product = productResponse.Message;
                        if (product.ProductId is null) AppleException.ThrowNotFound(message: "Product is not found.");

                        var cartItem = new Entities.CartItem
                        {
                            CartId = cart.Id,
                            ProductId = item.ProductId,
                            Quantity = item.Quantity,
                            UnitPrice = product.DiscountPrice.HasValue && product.DiscountPrice > 0 ? product.DiscountPrice.Value : product.Price ?? 0
                        };
                        cartItemRepository.Create(cartItem);
                    }
                });

                await Task.WhenAll(cartTasks);
                await cartItemRepository.SaveChangesAsync(cancellationToken);
                transaction.Commit();
                return Result<object>.Ok();
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
        }
    }
}