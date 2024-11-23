using AppleShop.order.commandApplication.Commands.Order;
using AppleShop.order.commandApplication.Validator.Order;
using AppleShop.order.Domain.Abstractions.IRepositories;
using AppleShop.Share.Enumerations;
using AppleShop.Share.Exceptions;
using AppleShop.Share.Shared;
using AutoMapper;
using MediatR;
using Entities = AppleShop.order.Domain.Entities;

namespace AppleShop.order.commandApplication.Handler.Order
{
    public class ChangeOrderStatusCommandHandler : IRequestHandler<ChangeOrderStatusCommand, Result<object>>
    {
        private readonly IOrderRepository orderRepository;
        private readonly IMapper mapper;

        public ChangeOrderStatusCommandHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            this.orderRepository = orderRepository;
            this.mapper = mapper;
        }

        public async Task<Result<object>> Handle(ChangeOrderStatusCommand request, CancellationToken cancellationToken)
        {
            var validator = new ChangeOrderStatusCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid) AppleException.ThrowValidation(validationResult);

            using var transaction = await orderRepository.BeginTransactionAsync(cancellationToken);
            try
            {
                var order = await orderRepository.FindByIdAsync(request.OrderId, true);
                if (order is null) AppleException.ThrowNotFound(typeof(Entities.Order));

                order.OrderStatus = ((OrderStatus)request.OrderStatus.Value).ToString();

                orderRepository.Update(order);
                await orderRepository.SaveChangesAsync(cancellationToken);

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