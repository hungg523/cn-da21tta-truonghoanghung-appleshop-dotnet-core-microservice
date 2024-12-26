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
    public class CreateUserAddressCommandHandler : IRequestHandler<CreateUserAddressCommand, Result<object>>
    {
        private readonly IUserAddressRepository userAddressRepository;
        private readonly IMapper mapper;

        public CreateUserAddressCommandHandler(IUserAddressRepository userAddressRepository, IMapper mapper)
        {
            this.userAddressRepository = userAddressRepository;
            this.mapper = mapper;
        }

        public async Task<Result<object>> Handle(CreateUserAddressCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateUserAddressCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid) AppleException.ThrowValidation(validationResult);

            var userAddress = mapper.Map<Entities.UserAddress>(request);
            using var transaction = await userAddressRepository.BeginTransactionAsync(cancellationToken);
            try
            {
                userAddressRepository.Create(userAddress);
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