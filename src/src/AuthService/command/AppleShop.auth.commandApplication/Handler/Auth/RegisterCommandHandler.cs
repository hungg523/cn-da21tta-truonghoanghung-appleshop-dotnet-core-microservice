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
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Result<object>>
    {
        private readonly IShareEventDispatcher shareEventDispatcher;
        private readonly IAuthRepository authRepository;
        private readonly IRequestClient<RegisterEvent> registerClient;

        public RegisterCommandHandler(IShareEventDispatcher shareEventDispatcher, IAuthRepository authRepository, IRequestClient<RegisterEvent> registerClient)
        {
            this.shareEventDispatcher = shareEventDispatcher;
            this.authRepository = authRepository;
            this.registerClient = registerClient;
        }

        public async Task<Result<object>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var validator = new RegisterCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid) AppleException.ThrowValidation(validationResult);

            using var transaction = await authRepository.BeginTransactionAsync(cancellationToken);
            try
            {
                var user = registerClient.GetResponse<RegisterResponse>(new RegisterEvent
                {
                    Email = request.Email,
                    Password = request.Password,
                }, cancellationToken);

                var errorMessage = user.Result.Message.Success switch
                {
                    1 => "Email has activated.",
                    _ => null
                };
                if (errorMessage is not null) AppleException.ThrowConflict(errorMessage);

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