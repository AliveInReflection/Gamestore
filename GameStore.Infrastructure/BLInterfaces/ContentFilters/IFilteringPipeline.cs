using System;
using System.Linq.Expressions;

namespace GameStore.Infrastructure.BLInterfaces
{
    public interface IFilteringPipeline<TEntity> where TEntity : class
    {
        void Add(IContentFilter<TEntity> filter);
        void Insert(IContentFilter<TEntity> filter, int position);
        Expression<Func<TEntity, bool>> GetExpression();

        bool IsEmpty();
    }
}
