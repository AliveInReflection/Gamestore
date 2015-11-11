using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using GameStore.DAL.Northwind.Interfaces;
using GameStore.Domain.Entities;

namespace GameStore.DAL.Northwind.Concrete
{
    public class NorthwindGenreRepository : INorthwindRepository<Genre>
    {
        private NorthwindContext context;

        public NorthwindGenreRepository(NorthwindContext context)
        {
            this.context = context;
        }

        public IEnumerable<Genre> GetAll(IEnumerable<int> idsToExclude)
        {
            var categories = context.Categories.ToList();
            var filteredCategories = categories.Where(m => !idsToExclude.Contains(m.CategoryID));
            return Mapper.Map<IEnumerable<Category>, IEnumerable<Genre>>(filteredCategories);
        }

        public Genre Get(int id)
        {
            var category = context.Categories.First(m => m.CategoryID.Equals(id));
            return Mapper.Map<Category, Genre>(category);
        }

        public int Count(IEnumerable<int> idsToExclude)
        {
            return context.Categories.Count(m => !idsToExclude.Contains(m.CategoryID));
        }
    }
}
