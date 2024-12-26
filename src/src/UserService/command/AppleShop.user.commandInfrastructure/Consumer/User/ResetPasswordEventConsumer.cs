using AppleShop.Share.Abstractions;
using AppleShop.Share.Events.User.Request;
using AppleShop.user.Domain.Abstractions.IRepositories;
using MassTransit;

namespace AppleShop.user.commandInfrastructure.Consumer.User
{
    public class ResetPasswordEventConsumer : IConsumer<ResetPasswordEvent>
    {
        private readonly IUserRepository userRepository;
        private readonly IEmailService emailService;

        public ResetPasswordEventConsumer(IUserRepository userRepository, IEmailService emailService)
        {
            this.userRepository = userRepository;
            this.emailService = emailService;
        }

        public async Task Consume(ConsumeContext<ResetPasswordEvent> context)
        {
            var message = context.Message;
            var user = await userRepository.FindSingleAsync(x => x.Email == message.Email, true);
            var newPassword = Guid.NewGuid().ToString().Substring(0, 8);
            var password = BCrypt.Net.BCrypt.HashPassword(newPassword);

            user.Password = password;
            userRepository.Update(user);
            await userRepository.SaveChangesAsync();

            var subject = "Đặt lại mật khẩu!";
            var body = $"<div style='font-family: Arial, sans-serif; max-width: 600px; margin: 0 auto; color: #333;'>\r\n    <div style='padding: 20px; border-bottom: 3px solid #007BFF; text-align: center;'>\r\n        <img src='https://drive.google.com/uc?export=view&id=1TLWTKoXTzjte2wyn0jnHjTrGUGdcbv98' alt='Logo' \r\n            style='width: 150px; margin-bottom: 10px;' />\r\n        <h2 style='color: #007BFF; font-weight: bold; margin: 0;'>Đặt lại mật khẩu</h2>\r\n    </div>\r\n    <div style='padding: 20px;'>\r\n        <p>Xin chào,<br>\r\n        Bạn đã yêu cầu đặt lại mật khẩu cho tài khoản của mình tại hệ thống của chúng tôi. Đây là mật khẩu mới của bạn:</p>\r\n        <div style='margin: 20px 0; padding: 15px; background-color: #e9ecef; border-radius: 8px; text-align: center; font-size: 24px; font-weight: bold; color: #007BFF;'>\r\n            {newPassword}\r\n        </div>\r\n        <p>Nếu bạn không yêu cầu đặt lại mật khẩu, vui lòng bỏ qua email này. Tài khoản của bạn sẽ không bị ảnh hưởng.</p>\r\n        <p>Trân trọng,<br />Hưng AppleShop!</p>\r\n    </div>\r\n    <div style='background-color: #343a40; color: white; padding: 10px; text-align: center; font-size: 12px; border-top: 3px solid #007BFF;'>\r\n        © 2024 Hưng AppleShop\r\n    </div>\r\n</div>";
            await emailService.SendEmailAsync(message.Email, subject, body);

            await context.ConsumeCompleted;
        }
    }
}