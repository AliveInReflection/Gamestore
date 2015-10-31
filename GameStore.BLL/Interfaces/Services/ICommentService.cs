using GameStore.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.BLL.Interfaces
{
    public interface ICommentService
    {
        void Create(string gameKey, CommentDTO comment);
        void Update(CommentDTO comment);
        void Delete(int commentId);

        IEnumerable<CommentDTO> Get(string gameKey);
        CommentDTO Get(int commentId);
    }
}
