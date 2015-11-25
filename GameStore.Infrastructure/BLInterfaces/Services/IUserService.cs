using System;
using System.Collections.Generic;
using GameStore.Infrastructure.DTO;

namespace GameStore.Infrastructure.BLInterfaces
{
    public interface IUserService
    {
        /// <summary>Add new entity to database</summary>
        /// <param name="user">Entity, received from UI</param>
        /// <exception>ValidationException</exception>
        void Create(UserDTO user);

        /// <summary>Update entity in database</summary>
        /// <param name="user">Entity, received from UI</param>
        /// <exception>ValidationException</exception>
        void Update(UserDTO user);

        /// <summary>Sets new password for entity in database</summary>
        /// <param name="model">Model for changing and verifiing and changing password</param>
        /// <exception>ValidationException</exception>
        void ChangePassword(ChangePasswordDTO model);

        /// <summary>Delete entity from database</summary>
        /// <param name="userId">Entity id received from UI</param>
        /// <exception>ValidationException</exception>
        void Delete(int userId);

        /// <summary>Returns user with specified id</summary>
        /// <param name="userId">User id</param>
        /// <returns>User</returns>
        /// <exception>ValidationException</exception>
        UserDTO Get(int userId);

        /// <summary>Returns user with specified name</summary>
        /// <param name="userName">company name</param>
        /// <returns>Publisher</returns>
        /// <exception>ValidationException</exception>
        UserDTO Get(string userName);

        /// <summary>Returns all users from database</summary>
        /// <returns>List of users</returns>
        IEnumerable<UserDTO> GetAll();


        /// <summary>Sets ban expiration date for user with specified id</summary>
        /// <param name="userId">User id</param>
        /// <param name="expirationDate">Ban duration</param>
        void Ban(int userId, TimeSpan expirationDate);

        /// <summary>Check existance of specified user name in database</summary>
        /// /// <param name="userName">User name</param>
        bool IsNameUsed(string userName);


        /// <summary>Returns all roles from database</summary>
        /// <returns>List of publishers</returns>
        IEnumerable<RoleDTO> GetAllRoles();

        /// <summary>Returns role with specified id</summary>
        /// <param name="roleId">Role id</param>
        /// <returns>Role</returns>
        /// <exception>ValidationException</exception>
        RoleDTO GetRole(int roleId);

        /// <summary>Add new entity to database</summary>
        /// <param name="role">Entity, received from UI</param>
        /// <exception>ValidationException</exception>
        void CreateRole(RoleDTO role);

        /// <summary>Update entity in database</summary>
        /// <param name="role">Entity, received from UI</param>
        /// <exception>ValidationException</exception>
        void UpdateRole(RoleDTO role);

        /// <summary>Delete entity from database</summary>
        /// <param name="roleId">Entity id received from UI</param>
        /// <exception>ValidationException</exception>
        void DeleteRole(int roleId);
    }
}
