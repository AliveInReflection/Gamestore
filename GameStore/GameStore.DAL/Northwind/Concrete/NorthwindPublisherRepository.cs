﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using GameStore.DAL.Northwind.Interfaces;
using GameStore.Domain.Entities;

namespace GameStore.DAL.Northwind.Concrete
{
    class NorthwindPublisherRepository : INorthwindRepository<Publisher>
    {
        private NorthwindContext context;

        public NorthwindPublisherRepository(NorthwindContext context)
        {
            this.context = context;
        }

        public IEnumerable<Publisher> GetAll(IEnumerable<int> idsToExclude)
        {
            var suppliers = context.Suppliers.ToList();
            var filteredSuppliers = suppliers.Where(m => !idsToExclude.Contains(m.SupplierID));
            return Mapper.Map<IEnumerable<Supplier>, IEnumerable<Publisher>>(filteredSuppliers);
        }

        public Publisher Get(int id)
        {
            var supplier = context.Suppliers.FirstOrDefault(m => m.SupplierID.Equals(id));
            return Mapper.Map<Supplier, Publisher>(supplier);
        }

        public int Count(IEnumerable<int> idsToExclude)
        {
            return context.Suppliers.Count(m => !idsToExclude.Contains(m.SupplierID));
        }
    }
}
