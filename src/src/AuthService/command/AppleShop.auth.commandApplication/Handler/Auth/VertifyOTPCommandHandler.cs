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
    public class VertifyOTPCommandHandler : IRequestHandler<VertifyOTPCommand, Result<object>>
    {
        private readonly IAuthRepository authRepository;
        private readonly IRequestClient<VertifyOTPEvent> vertifyOTPClient;
        private readonly IShareEventDispatcher shareEventDispatcher;

        public VertifyOTPCommandHandler(IAuthRepository authRepository, IShareEventDispatcher shareEventDispatcher, IRequestClient<VertifyOTPEvent> vertifyOTPClient)
        {
            this.authRepository = authRepository;
            this.shareEventDispatcher = shareEventDispatcher;
            this.vertifyOTPClient = vertifyOTPClient;
        }

        public async Task<Result<object>> Handle(VertifyOTPCommand request, CancellationToken cancellationToken)
        {
            var validator = new VertifyOTPCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid) AppleException.ThrowValidation(validationResult);

            using var transaction = await authRepository.BeginTransactionAsync(cancellationToken);
            try
            {
                transaction.Commit();
                var userVertify = vertifyOTPClient.GetResponse<VertifyOTPResponse>(new VertifyOTPEvent
                {
                    Email = request.Email ?? null,
                    OTP = request.OTP ?? null,
                }, cancellationToken);
                var errorMessage = userVertify.Result.Message.Success switch
                {
                    1 => "OTP is incorrect.",
                    2 => "OTP is expired.",
                    3 => "OTP has been entered more than 5 times.",
                    4 => "Email has activated",
                    _ => null
                };
                if (errorMessage is not null) AppleException.ThrowConflict(errorMessage);

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