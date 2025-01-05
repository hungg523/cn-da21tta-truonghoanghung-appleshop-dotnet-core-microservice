using AppleShop.Share.Events.User.Request;
using AppleShop.Share.Events.User.Response;
using AppleShop.user.Domain.Abstractions.IRepositories;
using MassTransit;

namespace AppleShop.user.commandInfrastructure.Consumer.User
{
    public class LoginEventConsumer : IConsumer<LoginEvent>
    {
        private readonly IUserRepository userRepository;

        public LoginEventConsumer(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task Consume(ConsumeContext<LoginEvent> context)
        {
            var message = context.Message;
            var user = await userRepository.FindSingleAsync(x => x.Email == message.Email, true);
            if (user is null)
            {
                await context.RespondAsync(new AuthResponse { Success = 1 });
                return;
            }
            bool isPassword = BCrypt.Net.BCrypt.Verify(message.Password, user.Password);
            if (!isPassword)
            {
                await context.RespondAsync(new AuthResponse { Success = 2 });
                return;
            }
            user.LastLogin = DateTime.Now;
            userRepository.Update(user);
            await userRepository.SaveChangesAsync();

            await context.RespondAsync(new AuthResponse { Success = 0 });
        }
    }
}