using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.DataAccess.Repository.IRepository;

namespace BookStore.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public ICategoryRepository iCategoryRepository { get; private set; }

        public ICoverTypeRepository iCoverTypeRepository { get; private set; }

        private readonly StoreContext _storeContext;

        public UnitOfWork(StoreContext storeContext)
        {
            _storeContext = storeContext;
            iCategoryRepository = new CategoryRepository(_storeContext);
            iCoverTypeRepository = new CoverTypeRepository(_storeContext);
        }

        public void Save()
        {
            _storeContext.SaveChanges();
        }
    }
}