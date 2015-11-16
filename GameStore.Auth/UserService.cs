using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using GameStore.DAL.Interfaces;
using GameStore.Domain.Entities;
using GameStore.Infrastructure.AuthInterfaces;
using GameStore.Infrastructure.DTO;
using GameStore.Logger.Interfaces;

namespace GameStore.Auth
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

        public void Ban(int userId, DateTime expirationDate)
        {
            var user = database.Users.Get(m => m.UserId.Equals(userId));
            
            user.BanExpirationDate = expirationDate;
            
            database.Users.Update(user);
            database.Save();
        }

        public IEnumerable<UserClaimDTO> GetClaims(int userId)
        {
            var claims = database.Users.Get(m => m.UserId.Equals(userId)).Claims;
            return Mapper.Map<IEnumerable<UserClaim>, IEnumerable<UserClaimDTO>>(claims);
        }
    }
}
