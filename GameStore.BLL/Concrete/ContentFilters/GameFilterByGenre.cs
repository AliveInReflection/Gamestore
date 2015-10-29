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
    public class GameFilterByGenre : IContentFilter<Game>
    {
        private IEnumerable<int> genreIds;

        public GameFilterByGenre(IEnumerable<int> genreIds)
        {
            this.genreIds = genreIds;
        }

        public Expression<Func<Game, bool>> GetExpression()
        {
            if (genreIds == null)
                throw new InvalidOperationException("No genres received");

            Expression<Func<Game, bool>> result = m => false;

            foreach (var genreId in genreIds)
            {
                Expression<Func<Game, bool>> currentExpression = m => m.Genres.Any(g => g.GenreId == genreId);
                result = result.CombineOr(currentExpression);
            }
            return result;
        }
    }
}
