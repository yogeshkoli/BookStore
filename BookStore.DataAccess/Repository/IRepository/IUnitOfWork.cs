using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.DataAccess.Repository.IRepository;

namespace BookStore.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        ICategoryRepository iCategoryRepository { get; }

        ICoverTypeRepository iCoverTypeRepository { get; }

        IProductRepository iProductRepository {get;}

        void Save();
    }
}