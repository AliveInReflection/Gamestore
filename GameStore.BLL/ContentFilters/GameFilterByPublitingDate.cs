using System;
using System.Linq.Expressions;
using GameStore.Domain.Entities;
using GameStore.Infrastructure.BLInterfaces;

namespace GameStore.BLL.Concrete.ContentFilters
{
    public class GameFilterByPublitingDate : IContentFilter<Game>
    {
        public DateTime minDate;

        public GameFilterByPublitingDate(TimeSpan timeSpan)
        {
            this.minDate = DateTime.UtcNow - timeSpan;
        }

        public Expression<Func<Game, bool>> GetExpression()
        {
            if (minDate == null)
                throw new InvalidOperationException("No date received");

            Expression<Func<Game, bool>> result = m => m.PublicationDate >= minDate;

            return result;
        }
    }
}
