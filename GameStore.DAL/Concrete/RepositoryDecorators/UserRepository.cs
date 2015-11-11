using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using Gamestore.DAL.Context;
using GameStore.DAL.Interfaces;
using GameStore.Domain.Entities;

namespace GameStore.DAL.Concrete.Repositories
{
    public class UserRepository : IRepository<User>
    {
        private GameStoreContext context;

        public UserRepository(GameStoreContext context)
        {
            this.context = context;
        }

        public void Create(User entity)
        {
            context.Entry(entity).State = EntityState.Added;
        }

        public void Update(User entity)
        {
            context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            var entry = context.Users.First(m => m.UserId.Equals(id));
            entry.IsDeleted = true;
        }

        public User Get(Expression<Func<User, bool>> predicate)
        {
            return context.Users.Where(predicate).First(m => !m.IsDeleted);
        }

        public IEnumerable<User> GetAll()
        {
            return context.Users.Where(m => !m.IsDeleted).ToList();
        }

        public IEnumerable<User> GetMany(Expression<Func<User, bool>> predicate)
        {
            return context.Users.Where(predicate).Where(m => !m.IsDeleted).ToList();
        }

        public int Count()
        {
            return context.Users.Count(m => !m.IsDeleted);
        }

        public bool IsExists(Expression<Func<User, bool>> predicate)
        {
            return context.Users.Where(predicate).Any(m => !m.IsDeleted);
        }
    }
}
