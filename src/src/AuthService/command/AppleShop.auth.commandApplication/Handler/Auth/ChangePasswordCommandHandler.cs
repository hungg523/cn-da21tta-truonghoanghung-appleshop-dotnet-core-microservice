using AppleShop.auth.commandApplication.Commands.Auth;
using AppleShop.auth.commandApplication.Validator.Auth;
using AppleShop.auth.Domain.Abstractions.IRepositories;
using AppleShop.Share.Abstractions;
using AppleShop.Share.Events.User.Request;
using AppleShop.Share.Events.User.Response;
using AppleShop.Share.Exceptions;
using AppleShop.Share.Shared;
using MassTransit;
using MediatR;

namespace AppleShop.auth.commandApplication.Handler.Auth
{
    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, Result<object>>
    {
        private readonly IAuthRepository authRepository;
        private readonly IRequestClient<UserRequest> userClient;
        private readonly IShareEventDispatcher shareEventDispatcher;

        public ChangePasswordCommandHandler(IAuthRepository authRepository, IRequestClient<UserRequest> userClient, IShareEventDispatcher shareEventDispatcher)
        {
            this.authRepository = authRepository;
            this.userClient = userClient;
            this.shareEventDispatcher = shareEventDispatcher;
        }

        public async Task<Result<object>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var validator = new ChangePasswordCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid) AppleException.ThrowValidation(validationResult);

            using var transaction = await authRepository.BeginTransactionAsync(cancellationToken);
            try
            {
                var userRequest = await userClient.GetResponse<UserResponse>(new UserRequest { Email = request.Email });
                var user = userRequest.Message;
                if (user.Success == 1) AppleException.ThrowNotFound();

                var changePasswordEvent = new ChangePasswordEvent
                {
                    Email = request.Email,
                    Password = request.NewPassword
                };
                await shareEventDispatcher.PublishAsync(changePasswordEvent);
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