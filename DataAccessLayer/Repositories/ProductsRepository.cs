using DataAccessLayer.Entities;
using DataAccessLayer.RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ProductsRepository(ApplicationDbContext applicationDbContext)
        {
            this._dbContext = applicationDbContext;
        }
        public async Task<Product?> AddProduct(Product product)
        {
            _dbContext.Products.Add(product);
            await _dbContext.SaveChangesAsync();
            return product;
        }

        public Task<bool> DeleteProduct(Guid productID)
        {
            throw new NotImplementedException();
        }

        public Task<Product?> GetProductByCondition(Expression<Func<Product, bool>> conditionExpressopn)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Product>> GetProducts()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Product?>> GetProductsByCondition(Expression<Func<Product, bool>> conditionExpressopn)
        {
            throw new NotImplementedException();
        }

        public Task<Product?> UpdateProduct(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
