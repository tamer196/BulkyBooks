using BulkyBooks.DataAccess.Repository.IRepository;
using BulkyBooks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBooks.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {

        private BulkyDbContext _db;

        public ProductRepository(BulkyDbContext db) : base(db)
        {
            _db= db;
        }

        public void Update(Product product)
        {
            var objFromDb = _db.Products.FirstOrDefault(u => u.Id == product.Id);

            if (objFromDb != null)
            {
                objFromDb.Title= product.Title;
                objFromDb.ISBN= product.ISBN;
                objFromDb.Price= product.Price;
                objFromDb.Price50= product.Price50;
                objFromDb.ListPrice= product.ListPrice;
                objFromDb.Price100= product.Price100;
                objFromDb.Description= product.Description;
                objFromDb.CategoryId= product.CategoryId;
                objFromDb.Author= product.Author;
                objFromDb.CoverTypeId= product.CoverTypeId;
                if (objFromDb.ImgUrl!= null)
                {
                    objFromDb.ImgUrl= product.ImgUrl;
                }
                
            }
        }
    }
}
