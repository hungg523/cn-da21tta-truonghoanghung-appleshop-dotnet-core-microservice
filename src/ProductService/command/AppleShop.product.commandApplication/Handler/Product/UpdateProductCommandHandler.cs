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
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Result<object>>
    {
        private readonly IProductRepository productRepository;
        private readonly IMapper mapper;
        private readonly IShareEventDispatcher shareEventDispatcher;
        private readonly IColorRepository colorRepository;
        private readonly IProductColorRepository productColorRepository;
        private readonly IRequestClient<GetCategoryByIdEvent> categoryClient;

        public UpdateProductCommandHandler(IProductRepository productRepository,
                                           IMapper mapper,
                                           IShareEventDispatcher shareEventDispatcher,
                                           IColorRepository colorRepository,
                                           IProductColorRepository productColorRepository,
                                           IRequestClient<GetCategoryByIdEvent> categoryClient)
        {
            this.productRepository = productRepository;
            this.mapper = mapper;
            this.shareEventDispatcher = shareEventDispatcher;
            this.colorRepository = colorRepository;
            this.productColorRepository = productColorRepository;
            this.categoryClient = categoryClient;
        }

        public async Task<Result<object>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateProductCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid) AppleException.ThrowValidation(validationResult);

            using var transaction = await productRepository.BeginTransactionAsync(cancellationToken);
            try
            {
                var product = await productRepository.FindByIdAsync(request.Id, true);
                if (product is null) AppleException.ThrowNotFound(typeof(Entities.Product));

                if (request.ColorIds is not null && request.ColorIds.Any()) await colorRepository.CheckIdsExistAsync(request.ColorIds.ToList());

                var dupColorId = request.ColorIds?.GroupBy(id => id).Where(g => g.Count() > 1).Select(g => g.Key).ToList();
                if (dupColorId!.Any()) AppleException.ThrowConflict();

                if (request.CategoryId is not null)
                {
                    var categoryRequest = await categoryClient.GetResponse<CategoryResponse>(new GetCategoryByIdEvent { Id = request.CategoryId });
                    var category = categoryRequest.Message;
                    if (category is null) AppleException.ThrowNotFound(message: "Category is not found.");
                }

                mapper.Map(request, product);

                if (request.ColorIds is not null)
                {
                    var existingProduct = productColorRepository.FindAll(x => x.ProductId == request.Id, true).ToList();
                    productColorRepository.RemoveMultiple(existingProduct);
                }
                product.ProductColors = request.ColorIds?.Distinct().Select(colorId => new Entities.ProductColor
                {
                    ProductId = product.Id,
                    ColorId = colorId
                }).ToList() ?? product.ProductColors;
                productRepository.Update(product);
                await productRepository.SaveChangesAsync(cancellationToken);

                var inventoryEvent = new InventoryUpdateEvent
                {
                    Stock = request.StockQuantity ?? 0,
                    ProductId = product.Id,
                    LastUpdated = DateTime.Now
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