using System.Collections.Generic;
using GameStore.Infrastructure.DTO;


namespace GameStore.Infrastructure.BLInterfaces
{
    public interface IPublisherService
    {
        /// <summary>Add new entity to database</summary>
        /// <param name="publisher">Entity, received from UI</param>
        /// <exception>ValidationException</exception>
        void Create(PublisherDTO publisher);

        /// <summary>Update entity in database</summary>
        /// <param name="publisher">Entity, received from UI</param>
        /// <exception>ValidationException</exception>
        void Update(PublisherDTO publisher);

        /// <summary>Delete entity from database</summary>
        /// <param name="publisherId">Entity id received from UI</param>
        /// <exception>ValidationException</exception>
        void Delete(int publisherId);


        /// <summary>Returns all publishers from database</summary>
        /// <returns>List of publishers</returns>
        IEnumerable<PublisherDTO> GetAll();

        /// <summary>Returns publisher with specified company name</summary>
        /// <param name="companyName">company name</param>
        /// <returns>Publisher</returns>
        /// <exception>ValidationException</exception>
        PublisherDTO Get(string companyName);

        /// <summary>Returns publisher with specified id</summary>
        /// <param name="publisherId">Publisher id</param>
        /// <returns>Publisher</returns>
        /// <exception>ValidationException</exception>
        PublisherDTO Get(int publisherId);
    }
}
