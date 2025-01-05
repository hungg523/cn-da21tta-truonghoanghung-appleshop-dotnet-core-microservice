using AppleShop.Share.Events.User.Request;
using AppleShop.Share.Events.User.Response;
using AppleShop.user.Domain.Abstractions.IRepositories;
using MassTransit;

namespace AppleShop.user.queryInfrastructure.Consumer
{
    public class GetInfoUserEventConsumer : IConsumer<GetInfoUserEvent>
    {
        private readonly IUserRepository userRepository;
        private readonly IUserAddressRepository userAddressRepository;

        public GetInfoUserEventConsumer(IUserRepository userRepository, IUserAddressRepository userAddressRepository)
        {
            this.userRepository = userRepository;
            this.userAddressRepository = userAddressRepository;
        }

        public async Task Consume(ConsumeContext<GetInfoUserEvent> context)
        {
            var message = context.Message;
            var users = userRepository.FindAll(x => message.UserId.Contains(x.Id)).ToList();
            var userAddresses = userAddressRepository.FindAll(x => message.UserAddressId.Contains(x.Id)).ToList();
            await context.RespondAsync(new GetInfoUserResponse
            {
                Users = users.Select(x => new UserResponse
                {
                    Id = x.Id,
                    Email = x.Email
                }).ToList(),
                UserAddresses = userAddresses.Select(x => new UserAddressResponse
                {
                    UserAddressId = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    PhoneNumber = x.PhoneNumber,
                    AddressLine = x.AddressLine,
                    Province = x.Province,
                    District = x.District
                }).ToList()
            });
        }
    }
}