using AppleShop.Share.Exceptions;
using AppleShop.Share.Shared;
using AppleShop.user.Domain.Abstractions.IRepositories;
using AppleShop.user.queryApplication.Queries.UserAddress;
using AppleShop.user.queryApplication.Validator.UserAddress;
using MediatR;
using Entities = AppleShop.user.Domain.Entities;

namespace AppleShop.user.queryApplication.Handler.UserAddress
{
    public class GetUserAddressByIdQueryHandler : IRequestHandler<GetUserAddressByIdQuery, Result<Entities.UserAddress>>
    {
        private readonly IUserAddressRepository userAddressRepository;

        public GetUserAddressByIdQueryHandler(IUserAddressRepository userAddressRepository)
        {
            this.userAddressRepository = userAddressRepository;
        }

        public async Task<Result<Entities.UserAddress>> Handle(GetUserAddressByIdQuery request, CancellationToken cancellationToken)
        {
            var validator = new GetUserAddressByIdQueryValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid) AppleException.ThrowValidation(validationResult);

            var userAddress = await userAddressRepository.FindByIdAsync(request.Id);
            if (userAddress is null) AppleException.ThrowNotFound(typeof(Entities.UserAddress));
            return Result<Entities.UserAddress>.Ok(userAddress);
        }
    }
}