using AppleShop.auth.commandApplication.Commands.Auth;
using AppleShop.auth.commandApplication.Validator.Auth;
using AppleShop.auth.Domain.Abstractions.IRepositories;
using Entities = AppleShop.auth.Domain.Entities;
using AppleShop.Share.Events.User.Request;
using AppleShop.Share.Events.User.Response;
using AppleShop.Share.Exceptions;
using AppleShop.Share.Shared;
using AutoMapper;
using MassTransit;
using MediatR;

namespace AppleShop.auth.commandApplication.Handler.Auth
{
    public class LogoutCommandHandler : IRequestHandler<LogoutCommand, Result<object>>
    {
        private readonly IAuthRepository authRepository;
        private readonly IRequestClient<UserRequest> userClient;
        private readonly IMapper mapper;

        public LogoutCommandHandler(IAuthRepository authRepository, IRequestClient<UserRequest> userClient, IMapper mapper)
        {
            this.authRepository = authRepository;
            this.userClient = userClient;
            this.mapper = mapper;
        }

        public async Task<Result<object>> Handle(LogoutCommand request, CancellationToken cancellationToken)
        {
            var validator = new LogoutCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid) AppleException.ThrowValidation(validationResult);

            using var transaction = await authRepository.BeginTransactionAsync(cancellationToken);
            try
            {
                var userRequest = await userClient.GetResponse<UserResponse>(new UserRequest { Email = request.Email });
                var user = userRequest.Message;
                if (user.Success == 1) AppleException.ThrowNotFound();

                var userToken = await authRepository.FindSingleAsync(x => x.UserId == user.Id, true);
                if (userToken is null) AppleException.ThrowNotFound(typeof(Entities.Auth));

                mapper.Map(request, userToken);
                userToken.IsActived = 0;
                authRepository.Update(userToken);
                await authRepository.SaveChangesAsync(cancellationToken);
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