﻿using System;
using System.Collections.Generic;
using GameStore.Infrastructure.DTO;

namespace GameStore.Infrastructure.BLInterfaces
{
    public interface IUserService
    {
        void Create(UserDTO user);
        void Update(UserDTO user);
        void Delete(int userId);

        UserDTO Get(int userId);
        UserDTO Get(string userName);

        IEnumerable<UserDTO> GetAll(); 

        void Ban(int userId, TimeSpan expirationDate);
        bool IsFree(string userName);
    }
}
