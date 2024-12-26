using AppleShop.auth.commandApplication.Commands.Auth;
using AppleShop.auth.commandApplication.Commands.DTOs;
using AppleShop.auth.Domain.Abstractions.IRepositories;
using AppleShop.Share.Abstractions;
using AppleShop.Share.Events.User.Request;
using AppleShop.Share.Events.User.Response;
using AppleShop.Share.Exceptions;
using AppleShop.Share.Shared;
using MassTransit;
using MediatR;
using System.Security.Claims;

namespace AppleShop.auth.commandApplication.Handler.Auth
{
    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, Result<LoginDTO>>
    {
        private readonly IAuthRepository authRepository;
        private readonly IJwtService jwtService;
        private readonly IRequestClient<GetUserByIdEvent> userClient;

        public RefreshTokenCommandHandler(IAuthRepository authRepository, IJwtService jwtService, IRequestClient<GetUserByIdEvent> userClient)
        {
            this.authRepository = authRepository;
            this.jwtService = jwtService;
            this.userClient = userClient;
        }

        public async Task<Result<LoginDTO>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            using var transaction = await authRepository.BeginTransactionAsync(cancellationToken);
            try
            {
                var authToken = await authRepository.FindSingleAsync(x => x.RefreshToken == request.RefreshToken && x.IsActived == 1, true);
                if (authToken is null || authToken.Expiration < DateTime.Now) AppleException.ThrowUnAuthorization("Invalid or expired refresh token.");

                var userRequest = await userClient.GetResponse<UserResponse>(new GetUserByIdEvent { Id = authToken.UserId });
                var userResponse = userRequest.Message;
                if (userResponse.Success == 1) AppleException.ThrowNotFound();

                var claims = new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, userResponse.Id.ToString()),
                    new Claim(ClaimTypes.Name, userResponse.Username),
                    new Claim(ClaimTypes.Role, userResponse.Role.ToString())
                };
                var newAccessToken = jwtService.GenerateAccessToken(claims);
                var newRefreshToken = jwtService.GenerateRefreshToken();
                authToken.RefreshToken = newRefreshToken;
                authToken.IssuedAt = DateTime.Now;
                authToken.Expiration = DateTime.Now.AddDays(7);
                authRepository.Create(authToken);
                await authRepository.SaveChangesAsync(cancellationToken);

                var response = new LoginDTO
                {
                    AccessToken = newAccessToken,
                    RefreshToken = newRefreshToken
                };

                transaction.Commit();
                return Result<LoginDTO>.Ok(response);
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
    }
}