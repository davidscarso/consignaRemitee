using CotizacionesAPI.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CotizacionesAPI.Middleware
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
