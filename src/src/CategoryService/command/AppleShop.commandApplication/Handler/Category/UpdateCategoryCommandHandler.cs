using AppleShop.commandApplication.Commands.Category;
using AppleShop.commandApplication.Validator.Category;
using AppleShop.Domain.Abstractions.IRepositories;
using AppleShop.Share.Abstractions;
using AppleShop.Share.Enumerations;
using AppleShop.Share.Exceptions;
using AppleShop.Share.Shared;
using AutoMapper;
using MediatR;
using Entities = AppleShop.Domain.Entities;

namespace AppleShop.commandApplication.Handler.Category
{
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, Result<object>>
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IFileService fileUploadService;
        private readonly IMapper mapper;

        public UpdateCategoryCommandHandler(ICategoryRepository categoryRepository, IFileService fileUploadService, IMapper mapper)
        {
            this.categoryRepository = categoryRepository;
            this.fileUploadService = fileUploadService;
            this.mapper = mapper;
        }

        public async Task<Result<object>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateCategoryCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid) AppleException.ThrowValidation(validationResult);

            using var transaction = await categoryRepository.BeginTransactionAsync(cancellationToken);
            try
            {
                var category = await categoryRepository.FindByIdAsync(request.Id);
                if (category is null) AppleException.ThrowNotFound(typeof(Entities.Category));
                if (request.IconData is not null)
                {
                    string fileName = (Path.GetFileName(category.Icon) is { } name &&
                            Path.GetExtension(name)?.ToLowerInvariant() == fileUploadService.GetFileExtensionFromBase64(request.IconData)?.ToLowerInvariant()) ? name : $"{Guid.NewGuid().ToString().Substring(0, 4)}{fileUploadService.GetFileExtensionFromBase64(request.IconData)}";
                    category.Icon = await fileUploadService.UploadFile(fileName, request.IconData, AssetType.CAT_ICON);
                }
                mapper.Map(request, category);
                categoryRepository.Update(category);

                await categoryRepository.SaveChangesAsync(cancellationToken);

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