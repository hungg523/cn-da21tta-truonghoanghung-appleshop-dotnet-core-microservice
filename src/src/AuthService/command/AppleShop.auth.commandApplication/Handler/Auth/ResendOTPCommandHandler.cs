using AppleShop.auth.commandApplication.Commands.Auth;
using AppleShop.auth.commandApplication.Validator.Auth;
using AppleShop.auth.Domain.Abstractions.IRepositories;
using AppleShop.Share.Events.User.Request;
using AppleShop.Share.Events.User.Response;
using AppleShop.Share.Exceptions;
using AppleShop.Share.Shared;
using MassTransit;
using MediatR;

namespace AppleShop.auth.commandApplication.Handler.Auth
{
    public class ResendOTPCommandHandler : IRequestHandler<ResendOTPCommand, Result<object>>
    {
        private readonly IAuthRepository authRepository;
        private readonly IRequestClient<ResendOTPEvent> resendOTPClient;

        public ResendOTPCommandHandler(IAuthRepository authRepository, IRequestClient<ResendOTPEvent> resendOTPClient)
        {
            this.authRepository = authRepository;
            this.resendOTPClient = resendOTPClient;
        }

        public async Task<Result<object>> Handle(ResendOTPCommand request, CancellationToken cancellationToken)
        {
            var validator = new ResendOTPCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid) AppleException.ThrowValidation(validationResult);

            using var transaction = await authRepository.BeginTransactionAsync(cancellationToken);
            try
            {
                var user = resendOTPClient.GetResponse<ResendOTPResponse>(new ResendOTPEvent { Email = request.Email }, cancellationToken);
                
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