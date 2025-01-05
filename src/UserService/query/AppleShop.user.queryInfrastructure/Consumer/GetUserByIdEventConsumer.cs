using AppleShop.Share.Events.User.Request;
using AppleShop.Share.Events.User.Response;
using AppleShop.user.Domain.Abstractions.IRepositories;
using MassTransit;

namespace AppleShop.user.queryInfrastructure.Consumer
{
    public class GetUserByIdEventConsumer : IConsumer<GetUserByIdEvent>
    {
        private readonly IUserRepository userRepository;

        public GetUserByIdEventConsumer(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task Consume(ConsumeContext<GetUserByIdEvent> context)
        {
            var message = context.Message;
            var user = await userRepository.FindByIdAsync(message.Id);
            if (user is null)
            {
                await context.RespondAsync(new UserResponse { Success = 1 });
                return;
            }
            await context.RespondAsync(new UserResponse
            {
                Id = user.Id,
                Email = user.Email,
                Username = user.Username,
                Role = user.Role,
                Success = 0
            });
        }
    }
}