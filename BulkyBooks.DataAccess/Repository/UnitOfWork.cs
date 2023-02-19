﻿using BulkyBooks.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBooks.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BulkyDbContext _db;
        public ICategoryRepository Category {get; private set;}

        public ICoverTypeRepository CoverType { get; private set;}
        public IProductRepository Product { get; set; }

        public UnitOfWork(BulkyDbContext db)
        {
            _db = db;
            Category = new CategoryRepository(_db);
            CoverType= new CoverTypeRepository(_db);
            Product = new ProductRepository(_db);

        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
