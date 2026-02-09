using AutoMapper;
using BusinessLogicLayer.DTO;
using BusinessLogicLayer.ServiceContract;
using DataAccessLayer.Entities;
using DataAccessLayer.RepositoryContracts;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System.Linq.Expressions;

namespace BusinessLogicLayer.Services
{
    internal class ProductService(IProductsRepository productsRepository,
        IMapper mapper, IValidator<ProductAddRequest> validatorAdd, IValidator<ProductUpdateRequest> validatorUpdate) : IProductService
    {
        private readonly IProductsRepository _productsRepository = productsRepository;
        private readonly IMapper _mapper = mapper;
        private readonly IValidator<ProductAddRequest> _ProductAddRequestValidator = validatorAdd; 
        private readonly IValidator<ProductUpdateRequest> _ProductUpdateRequestValidator = validatorUpdate;

        public async Task<ProductResponse?> AddProduct(ProductAddRequest productAddRequest)
        {
            if (productAddRequest == null) 
                throw new ArgumentNullException(nameof(productAddRequest));

            //validate the product req using fluent validation
           ValidationResult validationResult =  await 
                _ProductAddRequestValidator.ValidateAsync(productAddRequest);

            if (!validationResult.IsValid)
            {
                string error = string.Join(", ", validationResult.Errors
                        .Select(temp => temp.ErrorMessage));
                    throw new ArgumentException(error);
            }

            //attempt to add the product
            Product product = _mapper.Map<Product>(productAddRequest);
            Product? addedProduct =  await _productsRepository.AddProduct(product);

            if (addedProduct == null) return null;

            ProductResponse addedProductResponse =  _mapper.Map<ProductResponse>(addedProduct);
            return addedProductResponse;
        }

        public async Task<bool> DeleteProduct(Guid ProductID)
        {
            Product? existingProd =  await _productsRepository.
                GetProductByCondition(temp => temp.ProductID == ProductID);
            if (existingProd == null) return false;

            return await _productsRepository.DeleteProduct(ProductID);
        }

        public async Task<ProductResponse?> GetProductByCondition(Expression<Func<Product, bool>> expression)
        {
            Product? product = await _productsRepository.
               GetProductByCondition(expression);
            if(product == null) return null;

            ProductResponse productResponse = _mapper.Map<ProductResponse>(product);
            return productResponse;
        }

        public async Task<List<ProductResponse?>> GetProducts()
        {
           IEnumerable<Product?> products = await _productsRepository.
               GetProducts();

            IEnumerable<ProductResponse> productResponse =
                _mapper.Map<IEnumerable<ProductResponse>>(products);
            return [.. productResponse];
        }

        public async Task<List<ProductResponse?>> GetProductsByCondition(Expression<Func<Product, bool>> expression)
        {
            IEnumerable<Product?> products = await _productsRepository.
            GetProductsByCondition(expression);

            IEnumerable<ProductResponse> productResponse =
                _mapper.Map<IEnumerable<ProductResponse>>(products);
            return [.. productResponse];
        }

        public async Task<ProductResponse?> UpdateProduct(ProductUpdateRequest productUpdateRequest)
        {
            Product? existingProd = await _productsRepository.
               GetProductByCondition(temp => temp.ProductID == productUpdateRequest.ProductID);
            if (existingProd == null) throw new ArgumentException("Invalid product ID");

            ValidationResult validationResult = await _ProductUpdateRequestValidator.ValidateAsync(productUpdateRequest);

            if (!validationResult.IsValid)
            {
                string error = string.Join(", ", validationResult.Errors
                        .Select(temp => temp.ErrorMessage));
                throw new ArgumentException(error);
            }

            Product? updatedProduct =  _mapper.Map<Product?>(productUpdateRequest);
            Product? updated = await _productsRepository.UpdateProduct(updatedProduct);

            ProductResponse res = _mapper.Map<ProductResponse>(updated);
            return res; 
        }
    }
}
