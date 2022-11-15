using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Repository.Repositories.Interfaces;
using System.Linq.Expressions;


namespace Repository.Repositories
{
    public abstract class RepositoryBase<TEntity, TContext> : IRepositoryBase<TEntity> 
        where TEntity : class
        where TContext : DbContext
    {
        protected TContext Context { get; }
        
        private readonly DbSet<TEntity> entities;

        public RepositoryBase(TContext context)
        {
            this.Context = context ?? throw new ArgumentException(null, nameof(context));
            this.entities = context.Set<TEntity>();
        }

        public IQueryable<TEntity> FindAll(Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null, bool disableTracking = true)
        {
            IQueryable<TEntity> query = this.entities;

            if (disableTracking)
            {
                query = query.AsNoTracking();
            }

            if (include != null)
            {
                query = include(query);
            }

            return query;
        }

        public IQueryable<TEntity> FindByCondition(Expression<Func<TEntity, bool>> expression, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null, bool disableTracking = true)
        {
            IQueryable<TEntity> query = this.entities.Where(expression);

            if (disableTracking)
            {
                query = query.AsNoTracking();
            }

            if (include != null)
            {
                query = include(query);
            }

            return query;
        }
    }
}
