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
    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, Result<object>>
    {
        private readonly IRequestClient<UserRequest> userClient;
        private readonly IAuthRepository authRepository;
        private readonly IShareEventDispatcher shareEventDispatcher;

        public ResetPasswordCommandHandler(IRequestClient<UserRequest> userClient, IAuthRepository authRepository, IShareEventDispatcher shareEventDispatcher)
        {
            this.userClient = userClient;
            this.authRepository = authRepository;
            this.shareEventDispatcher = shareEventDispatcher;
        }

        public async Task<Result<object>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var validator = new ResetPasswordCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid) AppleException.ThrowValidation(validationResult);

            using var transaction = await authRepository.BeginTransactionAsync(cancellationToken);
            try
            {
                var userRequest = await userClient.GetResponse<UserResponse>(new UserRequest { Email = request.Email });
                var user = userRequest.Message;
                if (user.Success == 1) AppleException.ThrowNotFound();

                var userEvent = new ResetPasswordEvent { Email = request.Email };
                await shareEventDispatcher.PublishAsync(userEvent);
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