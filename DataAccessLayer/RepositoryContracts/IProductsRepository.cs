using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.RepositoryContracts
{
    public interface IProductsRepository
    {
        Task<IEnumerable<Product>> GetProducts();
        /// <summary>
        /// Retrives products based on a specified condition expressed as a lambda expression. The condition is defined as an Expression<Func<Product, bool>>, allowing for flexible querying of products that meet the criteria specified in the expression.
        /// </summary>
        /// <param name="conditionExpressopn"></param>
        /// <returns></returns>
        Task<IEnumerable<Product?>> GetProductsByCondition
            (Expression<Func<Product, bool>> conditionExpressopn);
        Task<Product?> GetProductByCondition
            (Expression<Func<Product, bool>> conditionExpressopn);
        Task<Product?> AddProduct(Product product);
        Task<Product?> UpdateProduct(Product product);
        Task<bool> DeleteProduct(Guid productID);
    }
}
