using BusinessLogicLayer.DTO;
using BusinessLogicLayer.ServiceContract;
using System.Runtime.CompilerServices;

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


            app.MapGet("/api/product/search/product-id/{ProductID}", async
            (IProductService productService,Guid ProductID) =>
            {
                List<ProductResponse?> products = await productService.GetProducts();
                return Results.Ok(products);
            });


            return app;
        }
    }
}
