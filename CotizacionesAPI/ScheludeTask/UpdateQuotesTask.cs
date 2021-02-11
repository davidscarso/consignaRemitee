using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using CotizacionesAPI.BackgroundService;
using CotizacionesAPI.Services;

namespace CotizacionesAPI.ScheludeTask
{
    public class UpdateQuotesTask : ScheduledProcessor
    {
        public UpdateQuotesTask(IServiceScopeFactory serviceScopeFactory) : base(serviceScopeFactory)
        {

        }

        protected override string Schedule => "*/10 * * * *"; // every 10 min 

        public override async Task ProcessInScopeAsync(IServiceProvider scopeServiceProvider)
        {
            //Console.WriteLine("Update Task!!! : " + DateTime.Now.ToString());

            CurrencylayerService dd = new CurrencylayerService();
            await dd.UpdateQuotesToBaseAsync();

            //return Task.CompletedTask;
        }
    }
}

