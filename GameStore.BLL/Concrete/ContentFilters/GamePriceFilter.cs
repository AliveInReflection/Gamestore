using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using GameStore.BLL.Interfaces.ContentFilters;
using GameStore.Domain.Entities;

namespace GameStore.BLL.ContentFilters
{
    public class GamePriceFilter : IContentFilter<Game>
    {
        private decimal minPrice;
        private decimal maxPrice;
        public GamePriceFilter(decimal min, decimal max)
        {
            this.minPrice = min;
            this.maxPrice = max;
        }
        public Expression<Func<Game, bool>> GetExpression()
        {
            Expression<Func<Game, bool>> result = m => m.Price >= minPrice && m.Price <= maxPrice;
            return result;
        }
    }
}
