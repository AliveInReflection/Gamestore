using System.Collections.Generic;
using GameStore.Infrastructure.DTO;


namespace GameStore.Infrastructure.BLInterfaces
{
    public interface IPlatformTypeService
    {
        /// <summary>Add new entity to database</summary>
        /// <param name="platformType">Entity, received from UI</param>
        /// <exception>ValidationException</exception>
        void Create(PlatformTypeDTO platformType);

        /// <summary>Update entity in database</summary>
        /// <param name="platformType">Entity, received from UI</param>
        /// <exception>ValidationException</exception>
        void Update(PlatformTypeDTO platformType);

        /// <summary>Delete entity from database</summary>
        /// <param name="platformTypeId">Entity id received from UI</param>
        /// <exception>ValidationException</exception>
        void Delete(int platformTypeId);

        /// <summary>Returns all platform types from database</summary>
        /// <returns>List of platform types</returns>
        IEnumerable<PlatformTypeDTO> GetAll();

        /// <summary>Returns list of platform types from database for game with specified game key</summary>
        /// <param name="gameKey">Game key</param>
        /// <returns>List of genres</returns>
        /// <exception>ValidationException</exception>
        IEnumerable<PlatformTypeDTO> Get(string gameKey);

        /// <summary>Returns platform type from database with specified id</summary>
        /// <param name="platformTypeId">Platform type id</param>
        /// <returns>Platform type</returns>
        /// <exception>ValidationException</exception>
        PlatformTypeDTO Get(int platformTypeId);
    }
}
