using AppleShop.Share.Events.User.Request;
using AppleShop.Share.Events.User.Response;
using AppleShop.user.Domain.Abstractions.IRepositories;
using MassTransit;

namespace AppleShop.user.commandInfrastructure.Consumer.User
{
    public class ChangePasswordEventConsumer : IConsumer<ChangePasswordEvent>
    {
        private readonly IUserRepository userRepository;

        public ChangePasswordEventConsumer(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task Consume(ConsumeContext<ChangePasswordEvent> context)
        {
            var message = context.Message;
            var user = await userRepository.FindSingleAsync(x => x.Email == message.Email, true);

            var password = BCrypt.Net.BCrypt.HashPassword(message.Password);
            user.Password = password;
            userRepository.Update(user);
            await userRepository.SaveChangesAsync();

            await context.ConsumeCompleted;
        }
    }
}