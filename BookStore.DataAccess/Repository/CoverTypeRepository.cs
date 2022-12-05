using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.DataAccess.Repository.IRepository;
using BookStore.Models;

namespace BookStore.DataAccess.Repository
{
    public class CoverTypeRepository : Repository<CoverType>, ICoverTypeRepository
    {

        private readonly StoreContext _storeConext;

        public CoverTypeRepository(StoreContext storeContext) : base(storeContext)
        {
            _storeConext = storeContext;
        }

        public void Update(CoverType coverType)
        {
            throw new NotImplementedException();
        }
    }
}