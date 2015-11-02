using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using GameStore.BLL.Infrastructure;
using GameStore.BLL.Interfaces.ContentFilters;
using GameStore.Domain.Entities;

namespace GameStore.BLL.Concrete.ContentFilters
{
    public class GameFilterByPublitingDate : IContentFilter<Game>
    {
        public DateTime minDate;

        public GameFilterByPublitingDate(TimeSpan timeSpan)
        {
            this.minDate = DateTime.UtcNow - timeSpan;
        }

        public System.Linq.Expressions.Expression<Func<Game, bool>> GetExpression()
        {
            if (minDate == null)
                throw new InvalidOperationException("No date received");

            Expression<Func<Game, bool>> result = m => m.PublicationDate >= minDate;

            return result;
        }
    }
}
