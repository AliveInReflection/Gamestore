using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gamestore.DAL.Context;
using GameStore.DAL.Interfaces;
using GameStore.Domain.Entities;

namespace GameStore.DAL.Concrete.Repositories
{
    public class PlatformTypeRepository : IRepository<PlatformType>
    {
        private GameStoreContext context;

        public PlatformTypeRepository(GameStoreContext context)
        {
            this.context = context;
        }

        public void Create(PlatformType entity)
        {
            context.Entry(entity).State = EntityState.Added;
        }

        public void Update(PlatformType entity)
        {
            context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            var entry = context.PlatformTypes.First(m => m.PlatformTypeId.Equals(id));
            entry.IsDeleted = true;
        }

        public PlatformType Get(System.Linq.Expressions.Expression<Func<PlatformType, bool>> predicate)
        {
            return context.PlatformTypes.Where(predicate).First(m => !m.IsDeleted);
        }

        public IEnumerable<PlatformType> GetAll()
        {
            return context.PlatformTypes.Where(m => !m.IsDeleted).ToList();
        }

        public IEnumerable<PlatformType> GetMany(System.Linq.Expressions.Expression<Func<PlatformType, bool>> predicate)
        {
            return context.PlatformTypes.Where(predicate).Where(m => !m.IsDeleted).ToList();
        }

        public int Count()
        {
            return context.PlatformTypes.Count(m => !m.IsDeleted);
        }

        public bool IsExists(System.Linq.Expressions.Expression<Func<PlatformType, bool>> predicate)
        {
            return context.PlatformTypes.Where(predicate).Any(m => !m.IsDeleted);
        }
    }
}
