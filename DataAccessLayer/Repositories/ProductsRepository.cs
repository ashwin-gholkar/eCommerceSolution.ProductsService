using DataAccessLayer.Entities;
using DataAccessLayer.RepositoryContracts;
using Microsoft.EntityFrameworkCore;
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

        public async Task<bool> DeleteProduct(Guid productID)
        {
            Product? exisitingProduct = await
                _dbContext.Products.FirstOrDefaultAsync(temp => temp.ProductID == productID);
            if (exisitingProduct == null)
            {
                return false;
            }
                _dbContext.Products.Remove(exisitingProduct);
                return await _dbContext.SaveChangesAsync() > 0 ? true : false;
        }

        public async Task<Product?> GetProductByCondition(Expression<Func<Product, bool>> conditionExpressopn)
        {
            return await _dbContext.Products
                        .FirstOrDefaultAsync(conditionExpressopn);
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _dbContext.Products
                         .ToListAsync();
        }

        public async Task<IEnumerable<Product?>> GetProductsByCondition(Expression<Func<Product, bool>> conditionExpressopn)
        {
            return await _dbContext.Products
                        .Where(conditionExpressopn).ToListAsync();
        }

        public async Task<Product?> UpdateProduct(Product product)
        {
            Product? exisitingProduct = await
                _dbContext.Products.FirstOrDefaultAsync(temp => temp.ProductID == product.ProductID);
            if (exisitingProduct == null)
            {
                return null;
            }
            exisitingProduct.ProductName = product.ProductName;
            exisitingProduct.Category = product.Category;
            exisitingProduct.UnitPrice = product.UnitPrice;
            exisitingProduct.QuantityInStock = product.QuantityInStock;

            await _dbContext.SaveChangesAsync();
            return exisitingProduct;

        }
    }
}
