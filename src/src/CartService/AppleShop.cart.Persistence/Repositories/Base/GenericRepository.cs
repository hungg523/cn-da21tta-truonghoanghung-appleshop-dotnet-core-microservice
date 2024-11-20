using AppleShop.cart.Domain.Abstractions.Common;
using AppleShop.cart.Domain.Abstractions.IRepositories.Base;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;
using System.Linq.Expressions;

namespace AppleShop.cart.Persistence.Repositories.Base
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly ApplicationDbContext dbContext;

        public GenericRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Delete(T entity)
        {
            dbContext.Set<T>().Remove(entity);
        }

        public void Update(T entity)
        {
            dbContext.Entry(entity).State = EntityState.Modified;
        }

        public void Create(T entity)
        {
            dbContext.Add(entity);
        }

        public IQueryable<T> FindAll(Expression<Func<T, bool>>? predicate = null, bool isTracking = false, params Expression<Func<T, object>>[] includeProperties)
        {
            var query = dbContext.Set<T>().AsQueryable();
            if (includeProperties.Any())
            {
                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }
            }
            query = isTracking ? query : query.AsNoTracking();
            return predicate is not null ? query.Where(predicate) : query;
        }

        public async Task<T?> FindByIdAsync(object id, bool isTracking = false, CancellationToken cancellationToken = default)
        {
            var query = dbContext.Set<T>().AsQueryable();
            query = isTracking ? query : query.AsNoTracking();

            var keyName = dbContext.Model.FindEntityType(typeof(T))?.FindPrimaryKey()?.Properties.First().Name;
            var result = await query.FirstOrDefaultAsync(x => EF.Property<object>(x, keyName).Equals(id), cancellationToken);
            return result;
        }

        public async Task<T?> FindSingleAsync(Expression<Func<T, bool>> predicate, bool isTracking = false, CancellationToken cancellationToken = default, params Expression<Func<T, object>>[] includeProperties)
        {
            var query = dbContext.Set<T>().AsQueryable();
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            query = isTracking ? query : query.AsNoTracking();
            var result = predicate is not null ? await query.FirstOrDefaultAsync(predicate, cancellationToken) : await query.FirstOrDefaultAsync(cancellationToken);
            return result;
        }

        public void RemoveMultiple(IEnumerable<T> entities)
        {
            dbContext.Set<T>().RemoveRange(entities);
        }
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) => dbContext.SaveChangesAsync(cancellationToken);

        public async Task<IDbTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            var transaction = await dbContext.Database.BeginTransactionAsync(cancellationToken);
            return transaction.GetDbTransaction();
        }
    }
}