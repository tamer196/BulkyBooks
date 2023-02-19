using BulkyBooks.DataAccess.Repository.IRepository;
using BulkyBooks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBooks.DataAccess.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private BulkyDbContext _db;

        public CategoryRepository(BulkyDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Category category)
        {
            _db.Categories.Update(category);
        }
    }
}
