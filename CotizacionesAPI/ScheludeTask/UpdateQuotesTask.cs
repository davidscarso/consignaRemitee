using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using CotizacionesAPI.BackgroundService;
using CotizacionesAPI.Services;

namespace CotizacionesAPI.ScheludeTask
{
    public class UpdateQuotesTask : ScheduledProcessor
    {
        private readonly ICurrencylayerService _currencylayerService;

        protected override string Schedule => "*/10 * * * *"; // every 10 min 


        public UpdateQuotesTask(ICurrencylayerService currencylayerService, IServiceScopeFactory serviceScopeFactory) : base(serviceScopeFactory)
        {
            _currencylayerService = currencylayerService;

        }


        public override async Task ProcessInScopeAsync(IServiceProvider scopeServiceProvider)
        {
            //Console.WriteLine("Update Task!!! : " + DateTime.Now.ToString());

            await _currencylayerService.UpdateQuotesToBaseAsync();

            //return Task.CompletedTask;
        }
    }
}

