using BusinessLogicLayer.DTO;
using BusinessLogicLayer.ServiceContract;
using DataAccessLayer.Entities;
using FluentValidation;
using FluentValidation.Results;

namespace ProductsService.API.APIEndpoints
{
    public static class ProductAPIEndpoints
    {
        public static IEndpointRouteBuilder MapProductAPIEndpoints(this IEndpointRouteBuilder app)
        {
            //GET /api/producrs
            app.MapGet("/api/products", async 
             (IProductService productService) =>
            {
                List<ProductResponse?> products = await productService.GetProducts();
                return Results.Ok(products);
            });

            //GET api/product/search/product-id/
            app.MapGet("/api/products/search/product-id/{ProductID}", async
            (IProductService productService,Guid ProductID) =>
            {
                ProductResponse? product = await productService.
                                            GetProductByCondition(x=>x.ProductID == ProductID);
                return Results.Ok(product);
            });


            //GET api/product/search/SearchString
            app.MapGet("/api/products/search/{SearchString}", async
                (IProductService productService, string SearchString) =>
            {

                List<ProductResponse?> productsByProductName = await productService.
                                            GetProductsByCondition(x =>  x.ProductName !=null &&
                                            x.ProductName.Contains
                                            (SearchString,StringComparison.OrdinalIgnoreCase));


                List<ProductResponse?> productsByCategoryName = await productService.
                                            GetProductsByCondition(x => x.Category != null &&
                                            x.Category.Contains
                                            (SearchString, StringComparison.OrdinalIgnoreCase));

                var products= productsByProductName.Union
                                (productsByCategoryName).ToList();


                return Results.Ok(products);

            });

            //POST api/products
            app.MapPost("/api/products", async
               (IProductService productService, IValidator<ProductAddRequest> 
               productAddValidator, ProductAddRequest productAddRequest) =>
            {
                //validation
                ValidationResult validationResult =  await productAddValidator.ValidateAsync(productAddRequest);

                if(!validationResult.IsValid)
                {
                    Dictionary<string, string[]> errors = validationResult.Errors
                                    .GroupBy(x=> x.PropertyName)
                                    .ToDictionary(grp => grp.Key,
                                                  grp => grp.Select(err => err.ErrorMessage)
                                                  .ToArray());

                    return Results.ValidationProblem(errors);
                }

                var addedProductResponse = await productService.AddProduct(productAddRequest);
                if(addedProductResponse != null)
                        return Results.Created($"/api/product/search/product-id/{addedProductResponse.ProductID}",addedProductResponse);
                else
                  return Results.Problem("Failed to add the product.");

            });


            //PUT api/products
            app.MapPut("/api/products", async
               (IProductService productService, IValidator<ProductUpdateRequest>
               productAddValidator, ProductUpdateRequest productUpdateRequest) =>
            {
                //validation
                ValidationResult validationResult = await productAddValidator.ValidateAsync(productUpdateRequest);

                if (!validationResult.IsValid)
                {
                    Dictionary<string, string[]> errors = validationResult.Errors
                                    .GroupBy(x => x.PropertyName)
                                    .ToDictionary(grp => grp.Key,
                                                  grp => grp.Select(err => err.ErrorMessage)
                                                  .ToArray());

                    return Results.ValidationProblem(errors);
                }

                var updatedProductResponse = await productService.UpdateProduct(productUpdateRequest);
                if (updatedProductResponse != null)
                    return Results.Ok( updatedProductResponse);
                else
                    return Results.Problem("Failed to updated the product.");

            });


            //DELETE api/products/product-id
            app.MapDelete("/api/products/{ProductID:guid}", async
              (IProductService productService, Guid ProductID) =>
            {
                

                bool isDeleted = await productService.DeleteProduct(ProductID);
                if (isDeleted)
                    return Results.Ok(true);
                else
                    return Results.Problem("Failed to delete the product.");

            });




            return app;
        }
    }
}
