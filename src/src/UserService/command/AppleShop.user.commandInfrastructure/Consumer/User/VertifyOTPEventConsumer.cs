using AppleShop.Share.Events.User.Request;
using AppleShop.Share.Events.User.Response;
using AppleShop.user.Domain.Abstractions.IRepositories;
using MassTransit;

namespace AppleShop.user.queryInfrastructure.Consumer
{
    public class VertifyOTPEventConsumer : IConsumer<VertifyOTPEvent>
    {
        private readonly IUserRepository userRepository;

        public VertifyOTPEventConsumer(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task Consume(ConsumeContext<VertifyOTPEvent> context)
        {
            var message = context.Message;
            var userExist = await userRepository.FindSingleAsync(x => x.Email == message.Email && x.IsActived == 1, true);
            if (userExist is not null)
            {
                await context.RespondAsync(new VertifyOTPResponse { Success = 4 });
            }
            var user = await userRepository.FindSingleAsync(x => x.Email == message.Email && x.OTP == message.OTP, true);
            if (user is null)
            {
                await context.RespondAsync(new VertifyOTPResponse { Success = 1 });
                user.OTPAttempt += 1;
                userRepository.Update(user);
                await userRepository.SaveChangesAsync();
                return;
            }
            if (user.OTPExpiration < DateTime.Now)
            {
                await context.RespondAsync(new VertifyOTPResponse { Success = 2 });
                return;
            }
            if (user.OTPAttempt > 5)
            {
                await context.RespondAsync(new VertifyOTPResponse { Success = 3 });
                return;
            }
            user.IsActived = 1;
            userRepository.Update(user);
            await userRepository.SaveChangesAsync();

            await context.RespondAsync(new VertifyOTPResponse { Success = 0 });
        }
    }
}