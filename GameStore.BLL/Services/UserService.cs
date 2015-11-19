using System;
using System.Collections.Generic;
using System.Security.Claims;
using AutoMapper;
using GameStore.BLL.Infrastructure;
using GameStore.DAL.Interfaces;
using GameStore.Domain.Entities;
using GameStore.Infrastructure.BLInterfaces;
using GameStore.Infrastructure.DTO;
using GameStore.Infrastructure.Enums;
using GameStore.Logger.Interfaces;

namespace GameStore.BLL.Services
{
    public class UserService : IUserService
    {
        private IUnitOfWork database;
        private IGameStoreLogger logger;

        public UserService(IUnitOfWork database, IGameStoreLogger logger)
        {
            this.database = database;
            this.logger = logger;
        }

        public bool IsFree(string userName)
        {
            return !database.Users.IsExists(m => m.UserName.ToLower().Equals(userName.ToLower()));
        }

        public void Ban(int userId, TimeSpan expirationDate)
        {
            var user = database.Users.Get(m => m.UserId.Equals(userId));

            user.BanExpirationDate = DateTime.UtcNow.Add(expirationDate);

            database.Users.Update(user);
            database.Save();
        }

        public void Create(UserDTO user)
        {
            if (user == null)
            {
                throw new ValidationException("No content received");
            }

            var userToSave = Mapper.Map<UserDTO, User>(user);
            userToSave.Roles = new[] {new Role() {RoleName = DefaultRoles.User}};
            try
            {
                database.Users.Create(userToSave);
                database.Save();
            }
            catch (InvalidOperationException)
            {
                throw new ValidationException(string.Format("User with the same name ({0}) is already existed", user.UserName));
            }
            
        }

        public void Update(UserDTO user)
        {
            throw new NotImplementedException();
        }

        public void Delete(int userId)
        {
            throw new NotImplementedException();
        }

        public UserDTO Get(int userId)
        {
            try
            {
                var user = database.Users.Get(m => m.UserId.Equals(userId));
                return Mapper.Map<User, UserDTO>(user);
            }
            catch (InvalidOperationException)
            {
                throw new ValidationException(string.Format("User not found(Id:{0})",userId));
            }
        }

        public UserDTO Get(string userName)
        {
            try
            {
                var user = database.Users.Get(m => m.UserId.Equals(userName));
                return Mapper.Map<User, UserDTO>(user);
            }
            catch (InvalidOperationException)
            {
                throw new ValidationException(string.Format("User not found(Name:{0})", userName));
            }
        }

        public IEnumerable<UserDTO> GetAll()
        {
            var users = database.Users.GetAll();
            return Mapper.Map<IEnumerable<User>, IEnumerable<UserDTO>>(users);
        }


        public IEnumerable<RoleDTO> GetAllRoles()
        {
            var roles = database.Roles.GetAll();
            return Mapper.Map<IEnumerable<Role>, IEnumerable<RoleDTO>>(roles);
        }

        public RoleDTO GetRole(int roleId)
        {
            try
            {
                var role = database.Roles.Get(m => m.RoleId.Equals(roleId));
                return Mapper.Map<Role, RoleDTO>(role);
            }
            catch (InvalidOperationException)
            {
                throw new ValidationException(string.Format("Role not found({0})", roleId));
            }
        }

        public void CreateRole(RoleDTO role)
        {
            if (role == null)
            {
                throw new ValidationException("No content received");
            }

            var roleToSave = Mapper.Map<RoleDTO, Role>(role);
            try
            {
                database.Roles.Create(roleToSave);
                database.Save();
            }
            catch (InvalidOperationException)
            {
                throw new ValidationException(string.Format("Role with the same name ({0}) is already existed", role.RoleName));
            }
        }

        public void UpdateRole(RoleDTO role)
        {
            throw new NotImplementedException();
        }

        public void DeleteRole(int roleId)
        {
            try
            {
                database.Roles.Delete(roleId);
                database.Save();
            }
            catch (InvalidOperationException)
            {
                throw new ValidationException(string.Format("Role not found({0})", roleId));
            }
        }
    }
}
