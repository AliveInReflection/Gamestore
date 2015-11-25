using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Infrastructure.DTO
{
    public class PaginationMode
    {
        public int ItemsPerPage { get; set; }
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
    }
}
