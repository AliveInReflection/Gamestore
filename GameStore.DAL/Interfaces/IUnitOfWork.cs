using GameStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<Game> Games { get;}
        IRepository<Comment> Comments { get; }
        IRepository<Genre> Genres { get; }
        IRepository<PlatformType> PlatformTypes { get; }

        void Save();
    }
}
