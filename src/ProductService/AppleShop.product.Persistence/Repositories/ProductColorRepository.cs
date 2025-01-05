﻿using AppleShop.product.Domain.Abstractions.IRepositories;
using AppleShop.product.Domain.Entities;
using AppleShop.product.Persistence.Repositories.Base;

namespace AppleShop.product.Persistence.Repositories
{
    public class ProductColorRepository(ApplicationDbContext context) : GenericRepository<ProductColor>(context), IProductColorRepository
    {
    }
}