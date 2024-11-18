using AppleShop.productCategory.commandApplication.Commands.Product;
using AppleShop.productCategory.commandApplication.Validator.Product;
using AppleShop.productCategory.Domain.Abstractions.IRepositories;
using AppleShop.Share.Abstractions;
using AppleShop.Share.Events.Inventory.Command;
using AppleShop.Share.Exceptions;
using AppleShop.Share.Shared;
using AutoMapper;
using MediatR;
using Entities = AppleShop.productCategory.Domain.Entities;

namespace AppleShop.productCategory.commandApplication.Handler.Product
{
    public class CreateProductCommandHanler : IRequestHandler<CreateProductCommand, Result<object>>
    {
        private readonly IProductRepository productRepository;
        private readonly ICategoryRepository categoryRepository;
        private readonly IMapper mapper;
        private readonly IShareEventDispatcher shareEventDispatcher;
        private readonly IColorRepository colorRepository;

        public CreateProductCommandHanler(IProductRepository productRepository, 
                                          ICategoryRepository categoryRepository, 
                                          IMapper mapper, 
                                          IShareEventDispatcher shareEventDispatcher, 
                                          IColorRepository colorRepository)
        {
            this.productRepository = productRepository;
            this.categoryRepository = categoryRepository;
            this.mapper = mapper;
            this.shareEventDispatcher = shareEventDispatcher;
            this.colorRepository = colorRepository;
        }

        public async Task<Result<object>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateProductCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid) AppleException.ThrowValidation(validationResult);
            
            if(request.CategoryId is not null)
            {
                var category = await categoryRepository.FindByIdAsync(request.CategoryId!);
                if (category is null) AppleException.ThrowNotFound(typeof(Entities.Category));
            }

            var product = mapper.Map<Entities.Product>(request);
            if (request.ColorIds is not null && request.ColorIds.Any()) await colorRepository.CheckIdsExistAsync(request.ColorIds.ToList());
            
            var dupColorId = request.ColorIds?.GroupBy(id => id).Where(g => g.Count() > 1).Select(g => g.Key).ToList();
            if (dupColorId!.Any()) AppleException.ThrowConflict();

            using var transaction = await productRepository.BeginTransactionAsync(cancellationToken);
            try
            {
                if (request.ColorIds is not null)
                {
                    product.ProductColors = request.ColorIds?.Distinct().Select(colorId => new Entities.ProductColor
                    {
                        ProductId = product.Id,
                        ColorId = colorId
                    }).ToList();
                }
                productRepository.Create(product);
                await productRepository.SaveChangesAsync(cancellationToken);
                transaction.Commit();
                var inventoryEvent = new InventoryCreateEvent
                {
                    Stock = request.StockQuantity,
                    ProductId = product.Id,
                    LastUpdated = DateTime.Now,
                };
                await shareEventDispatcher.PublishAsync(inventoryEvent);
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