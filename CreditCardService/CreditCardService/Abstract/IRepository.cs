using CreditCardService.Entities;
using System;
using System.Linq.Expressions;

namespace CreditCardService.Abstract
{
    public interface IRepository<TEntity>
    {
        TEntity Get(Expression<Func<TEntity, bool>> predicate);
        void Create(TEntity entity);
        void Update(TEntity entity);
        void Delete(int id);
    }
}