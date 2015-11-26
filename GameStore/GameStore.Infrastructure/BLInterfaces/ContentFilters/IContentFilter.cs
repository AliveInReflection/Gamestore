using System;
using System.Linq.Expressions;

namespace GameStore.Infrastructure.BLInterfaces
{
    public interface IContentFilter<TEntity> where TEntity : class
    {
        /// <summary>Returns expression for filtering entities</summary>
        /// <returns>Expression</returns>
        Expression<Func<TEntity, bool>> GetExpression();
    }
}
