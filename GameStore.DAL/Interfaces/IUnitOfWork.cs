using System;
using GameStore.Domain.Entities;

namespace GameStore.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Game> Games { get;}
        IRepository<Comment> Comments { get; }
        IRepository<Genre> Genres { get; }
        IRepository<PlatformType> PlatformTypes { get; }

        void Save();
    }
}
