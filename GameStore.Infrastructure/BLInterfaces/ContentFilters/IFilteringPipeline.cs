using System;
using System.Linq.Expressions;

namespace GameStore.Infrastructure.BLInterfaces
{
    public interface IFilteringPipeline<TEntity> where TEntity : class
    {
        /// <summary>Add received filter to pipeline</summary>
        /// <param name="filter">Instance of filter</param>
        void Add(IContentFilter<TEntity> filter);

        /// <summary>Insert received filter to pipeline to custom position</summary>
        /// <param name="filter">Instance of filter</param>
        /// <param name="position">Position</param>
        void Insert(IContentFilter<TEntity> filter, int position);

        /// <summary>Returns final expression by combining expressions from each filter</summary>
        /// <returns>Expression</returns>
        Expression<Func<TEntity, bool>> GetExpression();

        /// <summary>Check for existing at least one filter in pipeline</summary>
        bool IsEmpty();
    }
}
