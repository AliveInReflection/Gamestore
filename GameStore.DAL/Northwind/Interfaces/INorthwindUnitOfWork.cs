using GameStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.DAL.Northwind.Interfaces
{
    public interface INorthwindUnitOfWork
    {
        INorthwindRepository<Game> Games { get; }
        INorthwindRepository<Genre> Genres { get;}
        INorthwindRepository<Publisher> Publishers { get;}
        INorthwindRepository<Domain.Entities.Order> Orders { get; }
    }
}
