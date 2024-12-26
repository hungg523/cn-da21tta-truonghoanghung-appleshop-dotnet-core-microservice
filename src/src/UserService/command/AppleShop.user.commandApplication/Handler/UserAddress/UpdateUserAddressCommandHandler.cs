using AppleShop.Share.Exceptions;
using AppleShop.Share.Shared;
using AppleShop.user.commandApplication.Commands.UserAddress;
using AppleShop.user.commandApplication.Validator.UserAddress;
using AppleShop.user.Domain.Abstractions.IRepositories;
using AutoMapper;
using MediatR;
using Entities = AppleShop.user.Domain.Entities;

namespace AppleShop.user.commandApplication.Handler.UserAddress
{
    public class UpdateUserAddressCommandHandler : IRequestHandler<UpdateUserAddressCommand, Result<object>>
    {
        private readonly IUserAddressRepository userAddressRepository;
        private readonly IMapper mapper;

        public UpdateUserAddressCommandHandler(IUserAddressRepository userAddressRepository, IMapper mapper)
        {
            this.userAddressRepository = userAddressRepository;
            this.mapper = mapper;
        }

        public async Task<Result<object>> Handle(UpdateUserAddressCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateUserAddressCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid) AppleException.ThrowValidation(validationResult);

            using var transaction = await userAddressRepository.BeginTransactionAsync(cancellationToken);
            try
            {
                var userAddress = await userAddressRepository.FindByIdAsync(request.Id, true);
                if (userAddress is null) AppleException.ThrowNotFound(typeof(Entities.UserAddress));

                mapper.Map(request, userAddress);
                userAddressRepository.Update(userAddress);
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