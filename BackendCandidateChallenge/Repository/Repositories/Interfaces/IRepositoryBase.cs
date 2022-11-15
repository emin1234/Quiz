using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Repository.Repositories.Interfaces
{
    /// <summary>
    /// Generic repository for entity CRUD operations.
    /// </summary>
    /// <typeparam name="TEntity">Type of entity.</typeparam>
    internal interface IRepositoryBase<TEntity> where TEntity : class
    {
        /// <summary>
        /// Returns IQueryable for fetching all elements of specified entity.
        /// </summary>
        /// <param name="include">A function to include navigation properties.</param>
        /// <param name="disableTracking"><c>true</c> to disable changing tracking; otherwise, <c>false</c>. Default to <c>true</c>.</param>
        /// <returns>IQueriable of TEntity.</returns>
        IQueryable<TEntity> FindAll(Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null, bool disableTracking = true);

        /// <summary>
        /// Returns IQueryable for fetching elements that satisfy specified condition.
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="include">A function to include navigation properties.</param>
        /// <param name="disableTracking"><c>true</c> to disable changing tracking; otherwise, <c>false</c>. Default to <c>true</c>.</param>
        /// <returns>IQueryable of TEntity.</returns>
        IQueryable<TEntity> FindByCondition(Expression<Func<TEntity, bool>> expression, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null, bool disableTracking = true);
    }
}
