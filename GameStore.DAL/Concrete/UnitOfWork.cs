﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gamestore.DAL.Context;
using GameStore.DAL.Interfaces;
using GameStore.Domain.Entities;

namespace GameStore.DAL.Concrete
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly DataContext context;

        private IRepository<Game> games;
        private IRepository<Comment> comments;
        private IRepository<Genre> genres;
        private IRepository<PlatformType> platformTypes;


        public UnitOfWork(string connectionString)
        {
                context = new DataContext(connectionString);
        }


        public IRepository<Game> Games
        {
            get { return games ?? (games = new BaseRepository<Game>(context)); }
        }

        public IRepository<Comment> Comments
        {
            get { return comments ?? (comments = new BaseRepository<Comment>(context)); }
        }

        public IRepository<Genre> Genres
        {
            get { return genres ?? (genres = new BaseRepository<Genre>(context)); }
        }

        public IRepository<PlatformType> PlatformTypes
        {
            get { return platformTypes ?? (platformTypes = new BaseRepository<PlatformType>(context)); }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Dispose()
        {
            
        }
    }
}
