using AppleShop.Share.Abstractions;
using AppleShop.Share.Enumerations;
using AppleShop.Share.Exceptions;
using AppleShop.Share.Shared;
using AppleShop.user.commandApplication.Commands.User;
using AppleShop.user.commandApplication.Validator.User;
using AppleShop.user.Domain.Abstractions.IRepositories;
using AutoMapper;
using MediatR;
using Entities = AppleShop.user.Domain.Entities;

namespace AppleShop.user.commandApplication.Handler.User
{
    public class UpdateProfileUserCommandHandler : IRequestHandler<UpdateProfileUserCommand, Result<object>>
    {
        private readonly IUserRepository userRepository;
        private readonly IFileService fileUploadService;
        private readonly IMapper mapper;

        public UpdateProfileUserCommandHandler(IUserRepository userRepository, IFileService fileUploadService, IMapper mapper)
        {
            this.userRepository = userRepository;
            this.fileUploadService = fileUploadService;
            this.mapper = mapper;
        }

        public async Task<Result<object>> Handle(UpdateProfileUserCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateProfileUserCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid) AppleException.ThrowValidation(validationResult);

            using var transaction = await userRepository.BeginTransactionAsync(cancellationToken);
            try
            {
                var user = await userRepository.FindByIdAsync(request.Id, true);
                if (user is null) AppleException.ThrowNotFound(typeof(Entities.UserAddress));

                if (request.ImageData is not null)
                {
                    string fileName = (Path.GetFileName(user.ImageUrl) is { } name &&
                            Path.GetExtension(name)?.ToLowerInvariant() == fileUploadService.GetFileExtensionFromBase64(request.ImageData)?.ToLowerInvariant()) ? name : $"{Guid.NewGuid().ToString().Substring(0, 4)}{fileUploadService.GetFileExtensionFromBase64(request.ImageData)}";
                    user.ImageUrl = await fileUploadService.UploadFile(fileName, request.ImageData, AssetType.USER_IMG);
                }

                mapper.Map(request, user);
                userRepository.Update(user);
                await userRepository.SaveChangesAsync(cancellationToken);
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