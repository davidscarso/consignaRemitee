using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CotizacionesAPI.Services;
using static CotizacionesAPI.Services.CurrencylayerService;
using CotizacionesAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CotizacionesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyLayerController : ControllerBase
    {        
        private readonly ICurrencylayerService _currencylayerServiceService;

        /// <summary>
        /// Constructor
        /// </summary>
        public CurrencyLayerController(ICurrencylayerService currencylayerService)
        {     
            _currencylayerServiceService = currencylayerService;
        }

        // GET: api/CurrencyLayer
        /// <summary>
        /// Return a Currencylayer with all Quotes
        /// </summary>
        /// <returns>All Quotes</returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _currencylayerServiceService.GetQuotes();

            return Ok(result);
        }

        /// <summary>
        /// This method triggers the database update 
        /// </summary>
        /// <returns></returns>
        [HttpPut()]
        public async Task<IActionResult> PutUpdateQuotes()
        {
            var result = await _currencylayerServiceService.UpdateQuotesToBaseAsync();

            return Ok(result);
        }

    }
}
