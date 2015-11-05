using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using GameStore.DAL.Northwind.Interfaces;
using GameStore.Domain.Entities;

namespace GameStore.DAL.Northwind.Concrete
{
    class NorthwindGameRepository : INorthwindRepository<Game>
    {
        private NorthwindContext context;

        public NorthwindGameRepository(NorthwindContext context)
        {
            this.context = context;
        }

        public IEnumerable<Game> GetAll(IEnumerable<int> idsToExclude)
        {
            var products = context.Products.Where(m => !idsToExclude.Contains(m.ProductID)).ToList();
            return Mapper.Map<IEnumerable<Product>,IEnumerable<Game>>(products);
        }


        public Game Get(int id)
        {
            var product = context.Products.First(m => m.ProductID.Equals(id));
            return Mapper.Map<Product,Game>(product);
        }
    }
}
