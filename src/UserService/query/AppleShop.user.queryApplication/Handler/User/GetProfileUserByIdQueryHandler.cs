using AppleShop.Share.Exceptions;
using AppleShop.Share.Shared;
using AppleShop.user.Domain.Abstractions.IRepositories;
using AppleShop.user.queryApplication.Queries.DTOs;
using AppleShop.user.queryApplication.Queries.User;
using AppleShop.user.queryApplication.Validator.User;
using MediatR;
using Entities = AppleShop.user.Domain.Entities;

namespace AppleShop.user.queryApplication.Handler.User
{
    public class GetProfileUserByIdQueryHandler : IRequestHandler<GetProfileUserByIdQuery, Result<UserDTO>>
    {
        private readonly IUserRepository userRepository;

        public GetProfileUserByIdQueryHandler(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<Result<UserDTO>> Handle(GetProfileUserByIdQuery request, CancellationToken cancellationToken)
        {
            var validator = new GetProfileUserByIdQueryValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid) AppleException.ThrowValidation(validationResult);

            var user = await userRepository.FindByIdAsync(request.Id);
            if (user is null) AppleException.ThrowNotFound(typeof(Entities.User));

            var userDto = new UserDTO
            {
                Id = user.Id,
                Username = user.Username,
                ImageUrl = user.ImageUrl,
            };

            return Result<UserDTO>.Ok(userDto);
        }
    }
}