using AppleShop.Share.Events.User.Request;
using AppleShop.Share.Events.User.Response;
using AppleShop.user.Domain.Abstractions.IRepositories;
using MassTransit;

namespace AppleShop.user.queryInfrastructure.Consumer
{
    public class GetUserByEmailConsumer : IConsumer<UserRequest>
    {
        private readonly IUserRepository userRepository;

        public GetUserByEmailConsumer(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task Consume(ConsumeContext<UserRequest> context)
        {
            var message = context.Message;
            var user = await userRepository.FindSingleAsync(x => x.Email == message.Email);
            if (user is null)
            {
                await context.RespondAsync(new UserResponse { Success = 1 });
                return;
            }
            await context.RespondAsync(new UserResponse
            {
                Id = user.Id,
                Email = message.Email,
                Username = user.Username,
                Role = user.Role,
                Success = 0
            });
        }
    }
}