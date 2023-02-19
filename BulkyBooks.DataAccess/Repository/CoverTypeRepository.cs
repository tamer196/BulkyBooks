using BulkyBooks.DataAccess.Repository.IRepository;
using BulkyBooks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBooks.DataAccess.Repository
{
    public class CoverTypeRepository : Repository<CoverType>, ICoverTypeRepository
    {

        private BulkyDbContext _db;

        public CoverTypeRepository(BulkyDbContext db) : base(db)
        {
            _db= db;
        }

        public void Update(CoverType coverType)
        {
            _db.CoverTypes.Update(coverType);
        }
    }
}
