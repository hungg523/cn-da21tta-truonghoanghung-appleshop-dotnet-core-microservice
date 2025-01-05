using AppleShop.order.commandApplication.Commands.Order;
using AppleShop.order.commandApplication.Validator.Order;
using AppleShop.order.Domain.Abstractions.IRepositories;
using AppleShop.Share.Abstractions;
using AppleShop.Share.Enumerations;
using AppleShop.Share.Events.Order.Request;
using AppleShop.Share.Exceptions;
using AppleShop.Share.Shared;
using AutoMapper;
using MediatR;
using Entities = AppleShop.order.Domain.Entities;

namespace AppleShop.order.commandApplication.Handler.Order
{
    public class SuccessOrderCommandHandler : IRequestHandler<SuccessOrderCommand, Result<object>>
    {
        private readonly IOrderRepository orderRepository;
        private readonly IOrderItemRepository orderItemRepository;
        private readonly IMapper mapper;
        private readonly IShareEventDispatcher shareEventDispatcher;

        public SuccessOrderCommandHandler(IOrderRepository orderRepository, IOrderItemRepository orderItemRepository, IMapper mapper, IShareEventDispatcher shareEventDispatcher)
        {
            this.orderRepository = orderRepository;
            this.orderItemRepository = orderItemRepository;
            this.mapper = mapper;
            this.shareEventDispatcher = shareEventDispatcher;
        }

        public async Task<Result<object>> Handle(SuccessOrderCommand request, CancellationToken cancellationToken)
        {
            var validator = new SuccessOrderCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid) AppleException.ThrowValidation(validationResult);

            using var transaction = await orderRepository.BeginTransactionAsync(cancellationToken);
            try
            {
                var order = await orderRepository.FindByIdAsync(request.OrderId, true);
                if (order is null) AppleException.ThrowNotFound(typeof(Entities.Order));

                mapper.Map(request, order);
                order.OrderStatus = OrderStatus.SUCCESS.ToString();

                orderRepository.Update(order);
                await orderRepository.SaveChangesAsync(cancellationToken);

                var orderSuccess = orderRepository.FindAll(x => x.OrderStatus == OrderStatus.SUCCESS.ToString()).ToList();
                var orderIds = orderSuccess.Select(x => x.Id).ToList();
                var orderItems = orderItemRepository.FindAll(x => orderIds.Contains(x.OrderId)).ToList();
                var soldQuantity = orderItems.GroupBy(item => item.ProductId).ToDictionary(gr => gr.Key ?? 0, gr => gr.Sum(item => item.Quantity ?? 0));

                var productEvent = new UpdateSoldQuantityEvent { SoldQuantity = soldQuantity };
                await shareEventDispatcher.PublishAsync(productEvent);
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