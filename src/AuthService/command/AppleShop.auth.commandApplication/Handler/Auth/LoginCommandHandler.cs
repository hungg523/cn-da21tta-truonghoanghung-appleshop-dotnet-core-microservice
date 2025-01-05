using AppleShop.auth.commandApplication.Commands.Auth;
using AppleShop.auth.commandApplication.Commands.DTOs;
using AppleShop.auth.commandApplication.Validator.Auth;
using AppleShop.auth.Domain.Abstractions.IRepositories;
using AppleShop.Share.Abstractions;
using AppleShop.Share.Events.User.Request;
using AppleShop.Share.Events.User.Response;
using AppleShop.Share.Exceptions;
using AppleShop.Share.Shared;
using MassTransit;
using MediatR;
using System.Security.Claims;
using Entities = AppleShop.auth.Domain.Entities;

namespace AppleShop.auth.commandApplication.Handler.Auth
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, Result<LoginDTO>>
    {
        private readonly IAuthRepository authRepository;
        private readonly IJwtService jwtService;
        private readonly IRequestClient<UserRequest> userClient;
        private readonly IRequestClient<LoginEvent> loginClient;

        public LoginCommandHandler(IAuthRepository authRepository, IJwtService jwtService, IRequestClient<UserRequest> userClient, IRequestClient<LoginEvent> loginClient)
        {
            this.authRepository = authRepository;
            this.jwtService = jwtService;
            this.userClient = userClient;
            this.loginClient = loginClient;
        }

        public async Task<Result<LoginDTO>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var validator = new LoginCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid) AppleException.ThrowValidation(validationResult);

            using var transaction = await authRepository.BeginTransactionAsync(cancellationToken);
            try
            {
                var userRequest = await userClient.GetResponse<UserResponse>(new UserRequest { Email = request.Email });
                var user = userRequest.Message;
                if (user.Success == 1) AppleException.ThrowNotFound();
                
                var claims = new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, user.Role.ToString()),
                };
                var accessToken = jwtService.GenerateAccessToken(claims);
                var refreshToken = jwtService.GenerateRefreshToken();

                var userToken = new Entities.Auth
                {
                    UserId = user.Id,
                    RefreshToken = refreshToken,
                    IssuedAt = DateTime.Now,
                    Expiration = DateTime.Now.AddDays(7),
                    IsActived = 1
                };
                authRepository.Create(userToken);
                await authRepository.SaveChangesAsync(cancellationToken);

                var userLogin = await loginClient.GetResponse<AuthResponse>(new LoginEvent
                {
                    Email = request.Email,
                    Password = request.Password,
                }, cancellationToken);

                if (userLogin.Message.Success == 1) AppleException.ThrowNotFound();
                if (userLogin.Message.Success == 2) AppleException.ThrowUnAuthorization("Password is incorrect.");

                var response = new LoginDTO
                {
                    AccessToken = accessToken,
                    RefreshToken = refreshToken
                };
                transaction.Commit();
                return Result<LoginDTO>.Ok(response);
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
        }
    }
}