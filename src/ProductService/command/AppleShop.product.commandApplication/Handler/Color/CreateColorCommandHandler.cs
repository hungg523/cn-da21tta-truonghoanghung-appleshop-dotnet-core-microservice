﻿using AppleShop.product.commandApplication.Commands.Color;
using AppleShop.product.commandApplication.Validator.Color;
using AppleShop.product.Domain.Abstractions.IRepositories;
using AppleShop.Share.Exceptions;
using AppleShop.Share.Shared;
using AutoMapper;
using MediatR;
using Entities = AppleShop.product.Domain.Entities;

namespace AppleShop.product.commandApplication.Handler.Color
{
    public class CreateColorCommandHandler : IRequestHandler<CreateColorCommand, Result<object>>
    {
        private readonly IColorRepository colorRepository;
        private readonly IMapper mapper;

        public CreateColorCommandHandler(IColorRepository colorRepository, IMapper mapper)
        {
            this.colorRepository = colorRepository;
            this.mapper = mapper;
        }

        public async Task<Result<object>> Handle(CreateColorCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateColorCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid) AppleException.ThrowValidation(validationResult);

            var color = mapper.Map<Entities.Color>(request);
            using var transaction = await colorRepository.BeginTransactionAsync(cancellationToken);
            try
            {
                colorRepository.Create(color);
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