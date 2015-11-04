using System;
using System.Linq.Expressions;
using GameStore.Domain.Entities;
using GameStore.Infrastructure.BLInterfaces;

namespace GameStore.BLL.Concrete.ContentFilters
{
    class GameFilterByName : IContentFilter<Game>
    {
        private string partOfName;

        public GameFilterByName(string partOfName)
        {
            this.partOfName = partOfName;
        }
        public Expression<Func<Game, bool>> GetExpression()
        {
            if (String.IsNullOrEmpty(partOfName))
                throw new InvalidOperationException("No part of name received");

            Expression<Func<Game, bool>> result = m => m.GameName.Contains(partOfName);
            return result;
        }
    }
}
