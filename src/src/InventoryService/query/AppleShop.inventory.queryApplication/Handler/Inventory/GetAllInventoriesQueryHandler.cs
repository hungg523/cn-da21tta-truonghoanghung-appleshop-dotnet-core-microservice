using Entities = AppleShop.inventory.Domain.Entities;
using MediatR;
using AppleShop.Share.Shared;
using AppleShop.inventory.Domain.Abstractions.IRepositories;
using AppleShop.inventory.queryApplication.Queries.Inventory;

namespace AppleShop.inventory.queryApplication.Handler.Inventory
{
    public class GetAllInventoriesQueryHandler : IRequestHandler<GetAllInventoriesQuery, Result<List<Entities.Inventory>>>
    {
        private readonly IInventoryRepository iventoryRepository;

        public GetAllInventoriesQueryHandler(IInventoryRepository iventoryRepository)
        {
            this.iventoryRepository = iventoryRepository;
        }

        public async Task<Result<List<Entities.Inventory>>> Handle(GetAllInventoriesQuery request, CancellationToken cancellationToken)
        {
            var inventories = iventoryRepository.FindAll().ToList();
            return Result<List<Entities.Inventory>>.Ok(inventories);
        }
    }
}