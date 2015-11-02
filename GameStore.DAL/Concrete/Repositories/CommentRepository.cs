﻿using GameStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gamestore.DAL.Context;
using GameStore.DAL.Interfaces;
using System.Linq.Expressions;

namespace GameStore.DAL.Concrete.Repositories
{
    public class CommentRepository : IRepository<Comment>
    {
        private GameStoreContext context;

        public CommentRepository(GameStoreContext context)
        {
            this.context = context;
        }


        public void Create(Comment entity)
        {
            context.Entry(entity).State = EntityState.Added;
        }

        public void Update(Comment entity)
        {
            context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            var entry = context.Comments.First(m => m.CommentId.Equals(id));
            entry.IsDeleted = true;
        }

        public Comment Get(Expression<Func<Comment, bool>> predicate)
        {
            return context.Comments.Where(predicate).First(m => !m.IsDeleted);
        }

        public IEnumerable<Comment> GetAll()
        {
            return context.Comments.Where(m => !m.IsDeleted).ToList();
        }

        public IEnumerable<Comment> GetMany(Expression<Func<Comment, bool>> predicate)
        {
            return context.Comments.Where(predicate).Where(m => !m.IsDeleted).ToList();
        }

        public int Count()
        {
            return context.Comments.Count(m => !m.IsDeleted);
        }

        public bool IsExists(Expression<Func<Comment, bool>> predicate)
        {
            return context.Comments.Where(predicate).Any(m => !m.IsDeleted);
        }
    }
}