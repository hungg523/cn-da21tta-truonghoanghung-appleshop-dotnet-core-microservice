using AppleShop.inventory.Domain.Abstractions.IRepositories;
using AppleShop.inventory.queryApplication.Queries.Inventory;
using AppleShop.inventory.queryApplication.Validator.Inventory;
using AppleShop.Share.Exceptions;
using AppleShop.Share.Shared;
using MediatR;
using Entities = AppleShop.inventory.Domain.Entities;

namespace AppleShop.inventory.queryApplication.Handler.Category
{
    public class GetInventoryByIdQueryHandler : IRequestHandler<GetInventoryByIdQuery, Result<Entities.Inventory>>
    {
        private readonly IInventoryRepository iventoryRepository;

        public GetInventoryByIdQueryHandler(IInventoryRepository iventoryRepository)
        {
            this.iventoryRepository = iventoryRepository;
        }

        public async Task<Result<Entities.Inventory>> Handle(GetInventoryByIdQuery request, CancellationToken cancellationToken)
        {
            var validator = new GetInventoryByIdQueryValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid) AppleException.ThrowValidation(validationResult);

            var inventory = await iventoryRepository.FindByIdAsync(request.Id);
            if (inventory is null) AppleException.ThrowNotFound(typeof(Entities.Inventory));
            return Result<Entities.Inventory>.Ok(inventory);
        }
    }
}