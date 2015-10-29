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
    public class GameFilterByPlatformType : IContentFilter<Game>
    {
        private IEnumerable<int> platformTypeIds;

        public GameFilterByPlatformType(IEnumerable<int> platformTypeIds)
        {
            this.platformTypeIds = platformTypeIds;
        }

        public Expression<Func<Game, bool>> GetExpression()
        {
            if (platformTypeIds == null)
                throw new InvalidOperationException("No paltform types received");

            Expression<Func<Game, bool>> result = m => false;

            foreach (var platformTypeId in platformTypeIds)
            {
                Expression<Func<Game, bool>> currentExpression = m => m.PlatformTypes.Any(pt => pt.PlatformTypeId.Equals(platformTypeId));
                result = result.CombineOr(currentExpression);
            }
            return result;
        }
    }
}
