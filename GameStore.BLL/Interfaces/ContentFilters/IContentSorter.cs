﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.BLL.Interfaces.ContentFilters
{
    public interface IContentSorter<T>
    {
        IEnumerable<T> Sort(IEnumerable<T> source);
    }
}
