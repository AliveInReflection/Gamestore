using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using GameStore.BLL.Infrastructure;
using GameStore.BLL.Interfaces.ContentFilters;
using GameStore.Domain.Entities;

namespace GameStore.BLL.ContentFilters
{
    class GameFilterByPublisher : IContentFilter<Game>
    {
        private IEnumerable<int> publisherIds;

        public GameFilterByPublisher(IEnumerable<int> publisherIds)
        {
            this.publisherIds = publisherIds;
        }

        public Expression<Func<Game, bool>> GetExpression()
        {
            if (publisherIds == null)
                throw new InvalidOperationException("No publishers received");

            Expression<Func<Game, bool>> result = m => false;

            foreach (var publisherId in publisherIds)
            {
                Expression<Func<Game, bool>> currentExpression = m => m.Publisher.PublisherId.Equals(publisherId);               
                result = result.CombineOr(currentExpression);
            }
            return result;
        }
    }
}
