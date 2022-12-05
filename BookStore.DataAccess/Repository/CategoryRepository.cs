using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.DataAccess.Repository.IRepository;
using BookStore.Models;

namespace BookStore.DataAccess.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly StoreContext _storeContext;

        public CategoryRepository(StoreContext storeContext) : base(storeContext)
        {
            _storeContext = storeContext;
        }
        public void Save()
        {
            _storeContext.SaveChanges();
        }

        public void Update(Category category)
        {
            _storeContext.Update(category);
        }
    }
}