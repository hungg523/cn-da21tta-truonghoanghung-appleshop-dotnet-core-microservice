using AppleShop.Share.Abstractions;
using AppleShop.Share.Events.User.Request;
using AppleShop.Share.Events.User.Response;
using AppleShop.user.Domain.Abstractions.IRepositories;
using MassTransit;
using Entities = AppleShop.user.Domain.Entities;

namespace AppleShop.user.commandInfrastructure.Consumer.User
{
    public class RegisterEventConsumer : IConsumer<RegisterEvent>
    {
        private readonly IUserRepository userRepository;
        private readonly IEmailService emailService;

        public RegisterEventConsumer(IUserRepository userRepository, IEmailService emailService)
        {
            this.userRepository = userRepository;
            this.emailService = emailService;
        }

        public async Task Consume(ConsumeContext<RegisterEvent> context)
        {
            var message = context.Message;
            var userExist = await userRepository.FindSingleAsync(x => x.Email == message.Email && x.IsActived == 0, true);
            if (userExist is not null)
            {
                await context.RespondAsync(new AuthResponse { Success = 1 });
                return;
            }
            var otp = Guid.NewGuid().ToString().Substring(0, 6).ToUpper();
            var otpexpiration = DateTime.Now.AddSeconds(90);
            var password = BCrypt.Net.BCrypt.HashPassword(message.Password);
            var user = await userRepository.FindSingleAsync(x => x.Email == message.Email && x.IsActived == 0, true);

            if (user is not null)
            {
                user.Password = password;
                user.OTP = otp;
                user.OTPExpiration = otpexpiration;
                userRepository.Update(user);
            }
            else
            {
                user = new Entities.User
                {
                    Username = $"user_{Guid.NewGuid().ToString().Substring(0, 10)}",
                    Email = message.Email,
                    Password = password,
                    OTP = otp,
                    OTPAttempt = 0,
                    OTPExpiration = otpexpiration,
                    CreatedAt = DateTime.Now,
                    IsActived = 0,
                    Role = 0
                };

                userRepository.Create(user);
            }
            await userRepository.SaveChangesAsync();
            var subject = "Xác thực tài khoản!";
            var body = $"<div style='font-family: Arial, sans-serif; max-width: 600px; margin: 0 auto; color: #333;'>\r\n    <div style='padding: 20px; border-bottom: 3px solid #007BFF; text-align: center;'>\r\n        <img src='https://drive.google.com/uc?export=view&id=1TLWTKoXTzjte2wyn0jnHjTrGUGdcbv98' alt='Logo'\r\n            style='width: 150px; margin-bottom: 10px;' />\r\n        <h2 style='color: #007BFF; font-weight: bold; margin: 0;'>Xác thực tài khoản</h2>\r\n    </div>\r\n    <div style='padding: 20px;'>\r\n        <p>Xin chào,<br> Cảm ơn bạn đã đăng ký tài khoản tại hệ thống của chúng tôi. Để hoàn tất quá trình đăng ký, vui lòng sử dụng mã xác thực bên dưới:</p>\r\n        <div\r\n            style='margin: 20px 0; padding: 15px; background-color: #e9ecef; border-radius: 8px; text-align: center; font-size: 24px; font-weight: bold; color: #007BFF;'>\r\n            {otp}\r\n        </div>\r\n        <p>Nếu bạn không thực hiện yêu cầu này, vui lòng bỏ qua email này.</p>\r\n        <p>Trân trọng,<br />Hưng AppleShop!</p>\r\n    </div>\r\n    <div\r\n        style='background-color: #343a40; color: white; padding: 10px; text-align: center; font-size: 12px; border-top: 3px solid #007BFF;'>\r\n        © 2024 Hưng AppleShop\r\n    </div>\r\n</div>";
            await emailService.SendEmailAsync(message.Email, subject, body);

            await context.RespondAsync(new AuthResponse { Success = 0 });
        }
    }
}