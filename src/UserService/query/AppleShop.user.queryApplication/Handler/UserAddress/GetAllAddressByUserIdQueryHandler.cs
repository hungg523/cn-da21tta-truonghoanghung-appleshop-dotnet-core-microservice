using AppleShop.Share.Shared;
using Entities = AppleShop.user.Domain.Entities;
using AppleShop.user.queryApplication.Queries.UserAddress;
using MediatR;
using AppleShop.user.Domain.Abstractions.IRepositories;
using AppleShop.Share.Exceptions;
using AppleShop.user.queryApplication.Validator.UserAddress;

namespace AppleShop.user.queryApplication.Handler.UserAddress
{
    public class GetAllAddressByUserIdQueryHandler : IRequestHandler<GetAllAddressByUserIdQuery, Result<List<Entities.UserAddress>>>
    {
        private readonly IUserRepository userRepository;
        private readonly IUserAddressRepository userAddressRepository;

        public GetAllAddressByUserIdQueryHandler(IUserRepository userRepository, IUserAddressRepository userAddressRepository)
        {
            this.userRepository = userRepository;
            this.userAddressRepository = userAddressRepository;
        }

        public async Task<Result<List<Entities.UserAddress>>> Handle(GetAllAddressByUserIdQuery request, CancellationToken cancellationToken)
        {
            var validator = new GetAllAddressByUserIdQueryValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid) AppleException.ThrowValidation(validationResult);

            var user = await userRepository.FindByIdAsync(request.UserId);
            if (user is null) AppleException.ThrowNotFound(typeof(Entities.User));

            var userAddress = userAddressRepository.FindAll(x => x.UserId == request.UserId).ToList();
            return Result<List<Entities.UserAddress>>.Ok(userAddress);
        }
    }
}