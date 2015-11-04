using System.Collections.Generic;
using GameStore.Infrastructure.DTO;


namespace GameStore.Infrastructure.BLInterfaces
{
    public interface ICommentService
    {
        void Create(CommentDTO comment);
        void Update(CommentDTO comment);
        void Delete(int commentId);

        IEnumerable<CommentDTO> Get(string gameKey);
        CommentDTO Get(int commentId);
    }
}
