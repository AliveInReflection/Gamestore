using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.BLL.Interfaces.ContentFilters;
using GameStore.Domain.Entities;
using System.Linq.Expressions;
using GameStore.BLL.Infrastructure;

namespace GameStore.BLL.Concrete
{
    public class GameFilterPipeline : IFilteringPipeline<Game>
    {
        private List<IContentFilter<Game>> filters;

        public GameFilterPipeline()
        {
            filters = new List<IContentFilter<Game>>();
        }
 
        public void Add(IContentFilter<Game> filter)
        {
            filters.Add(filter);
        }

        public void Insert(IContentFilter<Game> filter, int position)
        {
            if (position < filters.Count)
            {
                filters.Insert(position, filter);
            }
            else
            {
                filters.Add(filter);
            }
        }

        public Expression<Func<Game, bool>> GetExpression()
        {
            Expression<Func<Game, bool>> result = m => true;
            foreach (var filter in filters)
            {
                result = result.CombineAnd(filter.GetExpression());
            }
            return result;
        }


        public bool IsEmpty()
        {
            return  filters.Count == 0;
        }
    }
}
