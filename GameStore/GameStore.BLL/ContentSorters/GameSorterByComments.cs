﻿using System.Collections.Generic;
using System.Linq;
using GameStore.Domain.Entities;
using GameStore.Infrastructure.BLInterfaces;

namespace GameStore.BLL.Concrete.ContentSorters
{
    public class GameSorterByComments : IContentSorter<Game>
    {
        public IEnumerable<Game> Sort(IEnumerable<Game> source)
        {
            return source.OrderByDescending(m => m.Comments.Count);
        }
    }
}
