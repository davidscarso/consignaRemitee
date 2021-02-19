using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CotizacionesAPI.Services
{
    public static class IoC
    {
        public static IServiceCollection AddDependency(this IServiceCollection services)
        {
            // Inyectar los servicios
            services.AddTransient<ICurrencylayerService, CurrencylayerService>();

            services.AddTransient<IQuoteService, QuoteService>();
            
            return services;
        }
    }
}
