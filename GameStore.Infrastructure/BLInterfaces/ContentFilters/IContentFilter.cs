using System;
using System.Linq.Expressions;

namespace GameStore.Infrastructure.BLInterfaces
{
    public interface IContentFilter<TEntity> where TEntity : class
    {
        Expression<Func<TEntity, bool>> GetExpression();
    }
}
