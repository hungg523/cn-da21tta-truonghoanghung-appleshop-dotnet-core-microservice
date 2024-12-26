using AppleShop.product.commandApplication.Commands.Color;
using AppleShop.product.commandApplication.Validator.Color;
using AppleShop.product.Domain.Abstractions.IRepositories;
using AppleShop.Share.Exceptions;
using AppleShop.Share.Shared;
using AutoMapper;
using MediatR;
using Entities = AppleShop.product.Domain.Entities;

namespace AppleShop.product.commandApplication.Handler.Color
{
    public class DeleteColorCommandHandler : IRequestHandler<DeleteColorCommand, Result<object>>
    {
        private readonly IColorRepository colorRepository;
        private readonly IMapper mapper;

        public DeleteColorCommandHandler(IColorRepository colorRepository, IMapper mapper)
        {
            this.colorRepository = colorRepository;
            this.mapper = mapper;
        }

        public async Task<Result<object>> Handle(DeleteColorCommand request, CancellationToken cancellationToken)
        {
            var validator = new DeleteColorCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid) AppleException.ThrowValidation(validationResult);

            using var transaction = await colorRepository.BeginTransactionAsync(cancellationToken);
            try
            {
                var color = await colorRepository.FindByIdAsync(request.Id, true);
                if (color is null) AppleException.ThrowNotFound(typeof(Entities.Color));

                colorRepository.Delete(color!);
                await colorRepository.SaveChangesAsync(cancellationToken);
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