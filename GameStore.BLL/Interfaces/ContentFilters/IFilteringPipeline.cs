using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.BLL.Interfaces.ContentFilters
{
    public interface IFilteringPipeline<TEntity> where TEntity : class
    {
        void Add(IContentFilter<TEntity> filter);
        void Insert(IContentFilter<TEntity> filter, int position);
        Expression<Func<TEntity, bool>> GetExpression();

        bool IsEmpty();
    }
}
