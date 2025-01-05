using AppleShop.category.Domain.Abstractions.Common;
using System.Data;
using System.Linq.Expressions;

namespace AppleShop.category.Domain.Abstractions.IRepositories.Base
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
        void RemoveMultiple(IEnumerable<T> entities);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        IQueryable<T> FindAll(Expression<Func<T, bool>>? predicate = null, bool isTracking = false, params Expression<Func<T, object>>[] includeProperties);
        Task<T?> FindByIdAsync(object id, bool isTracking = false, CancellationToken cancellationToken = default);
        Task<T?> FindSingleAsync(Expression<Func<T, bool>> predicate, bool isTracking = false, CancellationToken cancellationToken = default, params Expression<Func<T, object>>[] includeProperties);
        Task<IDbTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);
    }
}