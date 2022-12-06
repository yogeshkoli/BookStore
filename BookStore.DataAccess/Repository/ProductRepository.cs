using BookStore.DataAccess.Repository.IRepository;
using BookStore.Models;

namespace BookStore.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly StoreContext _storeContext;

        public ProductRepository(StoreContext storeContext) : base(storeContext)
        {
            _storeContext = storeContext;
        }

        public void Update(Product product)
        {
            var productObject = _storeContext.Products.FirstOrDefault(p => p.Id == product.Id);

            if (productObject != null)
            {
                productObject.Title = product.Title;
                productObject.ISBN = product.ISBN;
                productObject.ListPrice = product.ListPrice;
                productObject.Description = product.Description;
                productObject.Author = product.Author;
                productObject.Price = product.Price;
                productObject.Price50 = product.Price50;
                productObject.Price100 = product.Price100;
                productObject.CategoryId = product.CategoryId;
                productObject.CoverTypeId = product.CoverTypeId;

                if(product.ImageUrl != null)
                {
                    productObject.ImageUrl = product.ImageUrl;
                }

                _storeContext.Update(productObject);
            }
        }
    }
}