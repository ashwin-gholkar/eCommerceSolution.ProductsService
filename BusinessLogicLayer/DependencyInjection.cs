using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
        public static class DependencyInjection
        {
            public static IServiceCollection AddBusinessLogicLayer(this IServiceCollection services)
            {
                // Register your data access services here
                // For example:
                // services.AddScoped<IYourRepository, YourRepository>();
                return services;
            }
        }
    }
}
