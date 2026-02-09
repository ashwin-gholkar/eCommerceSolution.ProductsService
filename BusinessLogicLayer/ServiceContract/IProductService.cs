using BusinessLogicLayer.DTO;
using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.ServiceContract
{
    public interface IProductService
    {
        Task<List<ProductResponse?>> GetProducts();
        Task<List<ProductResponse?>> GetProductsByCondition
            (Expression<Func<Product, bool>> expression);

        Task<ProductResponse?> GetProductByCondition
            (Expression<Func<Product, bool>> expression);

        Task<ProductResponse?> AddProduct(ProductAddRequest productAddRequest);

        Task<ProductResponse?> UpdateProduct(ProductUpdateRequest productUpdateRequest);
        Task<bool> DeleteProduct(Guid ProductID);


    }
}
