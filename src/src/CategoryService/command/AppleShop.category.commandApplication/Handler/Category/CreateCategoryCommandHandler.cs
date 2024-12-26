using AppleShop.category.commandApplication.Commands.Category;
using AppleShop.category.commandApplication.Validator.Category;
using AppleShop.category.Domain.Abstractions.IRepositories;
using AppleShop.Share.Abstractions;
using AppleShop.Share.Enumerations;
using AppleShop.Share.Exceptions;
using AppleShop.Share.Shared;
using AutoMapper;
using MediatR;
using Entities = AppleShop.category.Domain.Entities;

namespace AppleShop.category.commandApplication.Handler.Category
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Result<object>>
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IFileService fileUploadService;
        private readonly IMapper mapper;

        public CreateCategoryCommandHandler(ICategoryRepository categoryRepository, IFileService fileUploadService, IMapper mapper)
        {
            this.categoryRepository = categoryRepository;
            this.fileUploadService = fileUploadService;
            this.mapper = mapper;
        }

        public async Task<Result<object>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateCategoryCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid) AppleException.ThrowValidation(validationResult);

            var category = mapper.Map<Entities.Category>(request);

            using var transaction = await categoryRepository.BeginTransactionAsync(cancellationToken);
            try
            {
                if (request.IconData is not null)
                {
                    var fileName = $"{Guid.NewGuid().ToString().Substring(0, 4)}{fileUploadService.GetFileExtensionFromBase64(request.IconData)}";
                    category.Icon = await fileUploadService.UploadFile(fileName, request.IconData, AssetType.CAT_ICON);
                }

                categoryRepository.Create(category);
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