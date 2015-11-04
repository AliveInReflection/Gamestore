using System;
using System.Linq.Expressions;
using GameStore.Domain.Entities;
using GameStore.Infrastructure.BLInterfaces;

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
