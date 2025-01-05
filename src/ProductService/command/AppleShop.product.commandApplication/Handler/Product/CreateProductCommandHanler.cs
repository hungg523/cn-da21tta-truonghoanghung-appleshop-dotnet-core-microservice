using AppleShop.product.commandApplication.Commands.Product;
using AppleShop.product.commandApplication.Validator.Product;
using AppleShop.product.Domain.Abstractions.IRepositories;
using AppleShop.Share.Abstractions;
using AppleShop.Share.Events.Category.Request;
using AppleShop.Share.Events.Category.Response;
using AppleShop.Share.Events.Inventory.Request;
using AppleShop.Share.Exceptions;
using AppleShop.Share.Shared;
using AutoMapper;
using MassTransit;
using MediatR;
using Entities = AppleShop.product.Domain.Entities;

namespace AppleShop.product.commandApplication.Handler.Product
{
    public class CreateProductCommandHanler : IRequestHandler<CreateProductCommand, Result<object>>
    {
        private readonly IProductRepository productRepository;
        private readonly IMapper mapper;
        private readonly IShareEventDispatcher shareEventDispatcher;
        private readonly IColorRepository colorRepository;
        private readonly IRequestClient<GetCategoryByIdEvent> categoryClient;

        public CreateProductCommandHanler(IProductRepository productRepository,
                                          IMapper mapper,
                                          IShareEventDispatcher shareEventDispatcher,
                                          IColorRepository colorRepository,
                                          IRequestClient<GetCategoryByIdEvent> categoryClient)
        {
            this.productRepository = productRepository;
            this.mapper = mapper;
            this.shareEventDispatcher = shareEventDispatcher;
            this.colorRepository = colorRepository;
            this.categoryClient = categoryClient;
        }

        public async Task<Result<object>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateProductCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid) AppleException.ThrowValidation(validationResult);
            
            if(request.CategoryId is not null)
            {
                var categoryRequest = await categoryClient.GetResponse<CategoryResponse>(new GetCategoryByIdEvent { Id = request.CategoryId });
                var category = categoryRequest.Message;
                if (category is null) AppleException.ThrowNotFound(message: "Category is not found.");
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
                var inventoryEvent = new InventoryCreateEvent
                {
                    Stock = request.StockQuantity,
                    ProductId = product.Id,
                    LastUpdated = DateTime.Now,
                };
                await shareEventDispatcher.PublishAsync(inventoryEvent);
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