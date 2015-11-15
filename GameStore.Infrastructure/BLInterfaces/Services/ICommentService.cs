using System.Collections.Generic;
using GameStore.Infrastructure.DTO;


namespace GameStore.Infrastructure.BLInterfaces
{
    public interface ICommentService
    {
        /// <summary>Add new entity to database</summary>
        /// <param name="comment">Entity, received from UI</param>
        /// <exception>ValidationException</exception>
        void Create(CommentDTO comment);

        /// <summary>Update entity in database</summary>
        /// <param name="comment">Entity, received from UI</param>
        /// <exception>ValidationException</exception>
        void Update(CommentDTO comment);

        /// <summary>Delete entity from database</summary>
        /// <param name="commentId">Entity id received from UI</param>
        /// <exception>ValidationException</exception>
        void Delete(int commentId);

        /// <summary>Returns all comments from database for game with specified game key</summary>
        /// <param name="gameKey">Game key</param>
        /// <returns>List of comments</returns>
        IEnumerable<CommentDTO> Get(string gameKey);

        /// <summary>Returns comment from database by specified id</summary>
        /// <param name="commentId">Comment id</param>
        /// <returns>Comment</returns>
        CommentDTO Get(int commentId);
    }
}
