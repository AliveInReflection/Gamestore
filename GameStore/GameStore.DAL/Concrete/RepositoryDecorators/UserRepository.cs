using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using Gamestore.DAL.Context;
using GameStore.DAL.Interfaces;
using GameStore.Domain.Entities;
using GameStore.Infrastructure.Enums;

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
            var roleNames = entity.Roles.Select(m => m.RoleName);
            var roles = context.Roles.Where(m => roleNames.Contains(m.RoleName)).ToList();
            entity.Roles = roles;
            context.Entry(entity).State = EntityState.Added;
        }

        public void Update(User entity)
        {
            var entry = context.Users.First(m => m.UserId.Equals(entity.UserId));

            if (entity.Country != null)
            {
                entry.Country = entity.Country;
            }

            if (entity.DateOfBirth != DateTime.MinValue)
            {
                entry.DateOfBirth = entity.DateOfBirth;
            }

            if (entity.Email != null)
            {
                entry.Email = entity.Email;
            }

            if (entity.PhoneNumber != null)
            {
                entry.PhoneNumber = entity.PhoneNumber;
            }

            if (entity.NotificationMethod != default(NotificationMethod))
            {
                entry.NotificationMethod = entity.NotificationMethod;
            }

            if (entity.BanExpirationDate != null)
            {
                entry.BanExpirationDate = entity.BanExpirationDate;
            }

            if (entity.Password != null)
            {
                entry.Password = entity.Password;
            }

            if (entity.Claims != null && entity.Claims.Any())
            {
                var oldClaims = entry.Claims.ToList();
                foreach (var userClaim in oldClaims)
                {
                    context.UserClaims.Remove(userClaim);
                }
                entry.Claims = entity.Claims;
            }

            if (entity.Roles != null && entity.Roles.Any())
            {
                var roleIds = entity.Roles.Select(m => m.RoleId);
                var roles = context.Roles.Where(m => roleIds.Contains(m.RoleId)).ToList();
                var oldRoles = entry.Roles.ToList();
                foreach (var role in oldRoles)
                {
                    entry.Roles.Remove(role);
                }
                entry.Roles = roles;
            }

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
