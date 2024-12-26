using AppleShop.cart.commandApplication.Commands.Cart;
using AppleShop.cart.commandApplication.Validator.Cart;
using AppleShop.cart.Domain.Abstractions.IRepositories;
using AppleShop.Share.Exceptions;
using AppleShop.Share.Shared;
using MediatR;
using Entities = AppleShop.cart.Domain.Entities;

namespace AppleShop.cart.commandApplication.Handler.Cart
{
    public class DeleteProductFromCartItemCommandHandler : IRequestHandler<DeleteProductFromCartItemCommand, Result<object>>
    {
        private readonly ICartRepository cartRepository;
        private readonly ICartItemRepository cartItemRepository;

        public DeleteProductFromCartItemCommandHandler(ICartRepository cartRepository, ICartItemRepository cartItemRepository)
        {
            this.cartRepository = cartRepository;
            this.cartItemRepository = cartItemRepository;
        }

        public async Task<Result<object>> Handle(DeleteProductFromCartItemCommand request, CancellationToken cancellationToken)
        {
            var validator = new DeleteProductFromCartItemCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid) AppleException.ThrowValidation(validationResult);

            using var transaction = await cartRepository.BeginTransactionAsync(cancellationToken);
            try
            {
                var cart = await cartRepository.FindByIdAsync(request.CartId, true);
                if (cart is null) AppleException.ThrowNotFound(typeof(Entities.Cart));

                var existingCartItem = await cartItemRepository.FindSingleAsync(x => x.CartId == cart.Id && x.ProductId == request.ProductId, true);
                if (existingCartItem is null) AppleException.ThrowNotFound(message: "Product is not found.");

                cartItemRepository.Delete(existingCartItem);
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