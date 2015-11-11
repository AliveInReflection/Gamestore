using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Gamestore.DAL.Context;
using GameStore.DAL.GameStore.Interfaces;
using GameStore.Domain.Entities;

namespace GameStore.DAL.GameStore.Concrete
{
    public class GameStoreCommentRepository : BaseGameStoreRepository<Comment>
    {
        public GameStoreCommentRepository(GameStoreContext context)
            :base(context)
        {
            
        }

        public override void Delete(int id)
        {
            var entry = context.Comments.First(m => m.CommentId.Equals(id));
            entry.IsDeleted = true;
        }

        public override void Update(Comment entity)
        {
            var entry = context.Comments.First(m => m.CommentId.Equals(entity.CommentId));
            Mapper.Map(entity, entry);
        }
    }
}
