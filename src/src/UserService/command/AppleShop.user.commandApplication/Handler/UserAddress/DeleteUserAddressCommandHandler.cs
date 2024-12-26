using AppleShop.Share.Exceptions;
using AppleShop.Share.Shared;
using AppleShop.user.commandApplication.Commands.UserAddress;
using AppleShop.user.commandApplication.Validator.UserAddress;
using AppleShop.user.Domain.Abstractions.IRepositories;
using MediatR;
using Entities = AppleShop.user.Domain.Entities;

namespace AppleShop.user.commandApplication.Handler.UserAddress
{
    public class DeleteUserAddressCommandHandler : IRequestHandler<DeleteUserAddressCommand, Result<object>>
    {
        private readonly IUserAddressRepository userAddressRepository;

        public DeleteUserAddressCommandHandler(IUserAddressRepository userAddressRepository)
        {
            this.userAddressRepository = userAddressRepository;
        }

        public async Task<Result<object>> Handle(DeleteUserAddressCommand request, CancellationToken cancellationToken)
        {
            var validator = new DeleteUserAddressCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid) AppleException.ThrowValidation(validationResult);

            using var transaction = await userAddressRepository.BeginTransactionAsync(cancellationToken);
            try
            {
                var userAddress = await userAddressRepository.FindByIdAsync(request.Id, true);
                if (userAddress is null) AppleException.ThrowNotFound(typeof(Entities.UserAddress));

                userAddressRepository.Delete(userAddress);
                await userAddressRepository.SaveChangesAsync(cancellationToken);
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