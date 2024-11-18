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
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Result<object>>
    {
        private readonly IProductRepository productRepository;
        private readonly ICategoryRepository categoryRepository;
        private readonly IMapper mapper;
        private readonly IShareEventDispatcher shareEventDispatcher;
        private readonly IColorRepository colorRepository;
        private readonly IProductColorRepository productColorRepository;

        public UpdateProductCommandHandler(IProductRepository productRepository, 
                                           ICategoryRepository categoryRepository, 
                                           IMapper mapper, 
                                           IShareEventDispatcher shareEventDispatcher, 
                                           IColorRepository colorRepository, 
                                           IProductColorRepository productColorRepository)
        {
            this.productRepository = productRepository;
            this.categoryRepository = categoryRepository;
            this.mapper = mapper;
            this.shareEventDispatcher = shareEventDispatcher;
            this.colorRepository = colorRepository;
            this.productColorRepository = productColorRepository;
        }

        public async Task<Result<object>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateProductCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid) AppleException.ThrowValidation(validationResult);

            using var transaction = await productRepository.BeginTransactionAsync(cancellationToken);
            try
            {
                var product = await productRepository.FindByIdAsync(request.Id);
                if (product is null) AppleException.ThrowNotFound(typeof(Entities.Product));

                if (request.ColorIds is not null && request.ColorIds.Any()) await colorRepository.CheckIdsExistAsync(request.ColorIds.ToList());

                var dupColorId = request.ColorIds?.GroupBy(id => id).Where(g => g.Count() > 1).Select(g => g.Key).ToList();
                if (dupColorId!.Any()) AppleException.ThrowConflict();

                if (request.CategoryId is not null)
                {
                    var category = await categoryRepository.FindByIdAsync(request.CategoryId);
                    if (category is null) AppleException.ThrowNotFound(typeof(Entities.Category));
                }

                mapper.Map(request, product);

                if (request.ColorIds is not null)
                {
                    var existingProduct = productColorRepository.FindAll(x => x.ProductId == request.Id).ToList();
                    productColorRepository.RemoveMultiple(existingProduct);
                }
                product.ProductColors = request.ColorIds?.Distinct().Select(colorId => new Entities.ProductColor
                {
                    ProductId = product.Id,
                    ColorId = colorId
                }).ToList() ?? product.ProductColors;
                productRepository.Update(product!);
                await productRepository.SaveChangesAsync(cancellationToken);
                transaction.Commit();
                var inventoryEvent = new InventoryUpdateEvent
                {
                    Stock = request.StockQuantity ?? 0,
                    ProductId = product.Id,
                    LastUpdated = DateTime.Now
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