using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.BLL.Interfaces.ContentFilters
{
    public interface IContentFilter<TEntity> where TEntity : class
    {
        Expression<Func<TEntity, bool>> GetExpression();
    }
}
