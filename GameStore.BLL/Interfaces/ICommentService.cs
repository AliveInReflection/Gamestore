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
        void AddComment(string gamekey, CommentDTO comment);
        IEnumerable<CommentDTO> GetCommentsByGameKey(string key);
    }
}
