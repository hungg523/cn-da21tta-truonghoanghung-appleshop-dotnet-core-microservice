using AppleShop.order.Domain.Abstractions.IRepositories;
using Entities = AppleShop.order.Domain.Entities;
using AppleShop.order.queryApplication.Queries.Order;
using AppleShop.order.queryApplication.Validator.Order;
using AppleShop.Share.Exceptions;
using AppleShop.Share.Shared;
using AutoMapper;
using MediatR;

namespace AppleShop.order.queryApplication.Handler.Order
{
    public class GetAllItemsByOrderIdQueryHandler : IRequestHandler<GetAllItemsByOrderIdQuery, Result<List<Entities.OrderItem>>>
    {
        private readonly IOrderItemRepository orderitemRepository;
        private readonly IOrderRepository orderRepository;
        private readonly IMapper mapper;

        public GetAllItemsByOrderIdQueryHandler(IOrderItemRepository orderitemRepository, IOrderRepository orderRepository, IMapper mapper)
        {
            this.orderitemRepository = orderitemRepository;
            this.orderRepository = orderRepository;
            this.mapper = mapper;
        }

        public async Task<Result<List<Entities.OrderItem>>> Handle(GetAllItemsByOrderIdQuery request, CancellationToken cancellationToken)
        {
            var validator = new GetAllItemsByOrderIdQueryValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid) AppleException.ThrowValidation(validationResult);

            var order = await orderRepository.FindByIdAsync(request.OrderId);
            if (order is null) AppleException.ThrowNotFound(typeof(Entities.Order));

            var orderItem = orderitemRepository.FindAll(x => x.OrderId == request.OrderId).ToList();
            return Result<List<Entities.OrderItem>>.Ok(orderItem);
        }
    }
}