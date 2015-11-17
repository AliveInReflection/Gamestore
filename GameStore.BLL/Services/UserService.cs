using System;
using System.Collections.Generic;
using System.Security.Claims;
using AutoMapper;
using GameStore.DAL.Interfaces;
using GameStore.Domain.Entities;
using GameStore.Infrastructure.BLInterfaces;
using GameStore.Infrastructure.DTO;
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

        public IEnumerable<Claim> GetClaims(int userId)
        {
            var claims = database.Users.Get(m => m.UserId.Equals(userId)).Claims;
            return Mapper.Map<IEnumerable<UserClaim>, IEnumerable<Claim>>(claims);
        }


        public IEnumerable<Claim> GetClaims(string role)
        {
            throw new NotImplementedException();
        }

        public void Create(GameStore.Infrastructure.DTO.UserDTO user)
        {
            throw new NotImplementedException();
        }

        public void Update(GameStore.Infrastructure.DTO.UserDTO user)
        {
            throw new NotImplementedException();
        }

        public void Delete(int userId)
        {
            throw new NotImplementedException();
        }

        public UserDTO Get(int userId)
        {
            throw new NotImplementedException();
        }

        public UserDTO Get(string userName)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserDTO> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
