using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.Domain.Entities.Domain.Entities;

namespace GameStore.Domain.Entities.Domain.Entities.DAL.Interfaces
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
