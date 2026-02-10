using BusinessLogicLayer.Mappers;
using BusinessLogicLayer.ServiceContract;
using BusinessLogicLayer.Services;
using BusinessLogicLayer.Validators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace BusinessLogicLayer
{
        public static class DependencyInjection
        {
            public static IServiceCollection AddBusinessLogicLayer(this IServiceCollection services)
            {
            // Register your data access services here
            // For example:
            services.AddAutoMapper(cfg =>
            { },
            typeof(ProductAddRequestToProductMappingProfile).Assembly);

            services.AddValidatorsFromAssemblyContaining<ProdcutAddRequestValidator>();
            services.AddScoped<IProductService, ProductService>();
            
                return services;
            }
        }
}
