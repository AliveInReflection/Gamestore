using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using Gamestore.DAL.Context;
using GameStore.DAL.Interfaces;
using GameStore.Domain.Entities;

namespace GameStore.DAL.Concrete.RepositoryDecorators
{
    public class RoleRepository : IRepository<Role>
    {
        private GameStoreContext context;

        public RoleRepository(GameStoreContext context)
        {
            this.context = context;
        }

        public void Create(Role entity)
        {
            context.Roles.Add(entity);
        }

        public void Update(Role entity)
        {
            var entry = context.Roles.First(m => m.RoleId.Equals(entity.RoleId));
            entry.RoleName = entity.RoleName;

            var oldClaims = entry.Claims.ToList();
            foreach (var claim in oldClaims)
            {
                context.RoleClaims.Remove(claim);
            }
            entry.Claims = entity.Claims;
        }

        public void Delete(int id)
        {
            var entry = context.Roles.First(m => m.RoleId.Equals(id));
            entry.IsDeleted = true;
        }

        public Role Get(Expression<Func<Role, bool>> predicate)
        {
            return context.Roles.Where(m => !m.IsDeleted).First(predicate);
        }

        public IEnumerable<Role> GetAll()
        {
            return context.Roles.Where(m => !m.IsDeleted).ToList();
        }

        public IEnumerable<Role> GetMany(Expression<Func<Role, bool>> predicate)
        {
            return context.Roles.Where(m => !m.IsDeleted).Where(predicate).ToList();
        }

        public int Count()
        {
            return context.Roles.Count(m => !m.IsDeleted);
        }

        public bool IsExists(Expression<Func<Role, bool>> predicate)
        {
            return context.Roles.Where(m => !m.IsDeleted).Any(predicate);
        }
    }
}
