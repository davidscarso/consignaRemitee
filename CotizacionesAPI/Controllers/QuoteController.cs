using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CotizacionesAPI.Models;
using CotizacionesAPI.Services;

namespace CotizacionesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuoteController : ControllerBase
    {
        private readonly IQuoteService _quoteService;

        /// <summary>
        /// Constructor
        /// </summary>
        public QuoteController(IQuoteService quoteService)
        {
            _quoteService = quoteService;
        }

        // GET: api/Quote
        [HttpGet]
        public async Task<IEnumerable<QuoteModel>> GetQuotes()
        {
            return await _quoteService.GetAll();
        }

        // GET: api/Quote/5
        [HttpGet("{id}")]
        public IActionResult GetQuoteModel([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var quoteModel = _quoteService.GetOne(id);

            if (quoteModel == null)
            {
                return NotFound();
            }

            return Ok(quoteModel);
        }

        // PUT: api/Quote/5
        [HttpPut("{id}")]
        public IActionResult PutQuoteModel([FromRoute] string id, [FromBody] QuoteModel quoteModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != quoteModel.Id)
            {
                return BadRequest();
            }

            try
            {
                var reult = _quoteService.PutOne(id, quoteModel);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_quoteService.Exists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Quote
        [HttpPost]
        public async Task<IActionResult> PostQuoteModel([FromBody] QuoteModel quoteModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(await _quoteService.PostOne(quoteModel));

            //return CreatedAtAction("GetQuoteModel", new { id = quoteModel.Id }, quoteModel);
        }

        // DELETE: api/Quote/5
        [HttpDelete("{id}")]
        public IActionResult DeleteQuoteModel([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_quoteService.Exists(id))
            {
                return NotFound();
            }

            var result = _quoteService.DeleteOne(id);

            return Ok(result);
        }

        #region CALCULATIOMS

        /// <summary>
        /// Calcular el monto X a enviar en moneda M para que llegue el monto Y en moneda N.
        /// </summary>
        /// <param name="sourse">ej USD</param>
        /// <param name="valuein">ej 100.10</param>
        /// <param name="money">ARS</param>
        /// <returns></returns>
        [HttpGet]
        [Route("calculation/{sourse}/from/{valuein}/{money}")]
        public IActionResult GetQuoteCalculationX(string sourse, double valuein, string money)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = _quoteService.CalculationX(sourse, valuein, money);

            if (result == 0)
            {
                return NotFound();
            }

            return Ok(result);
        }
        /// <summary>
        /// Calcular el monto Y a recibir en moneda N enviando el monto X en moneda M.
        /// </summary>
        /// <param name="sourse"></param>
        /// <param name="valuein"></param>
        /// <param name="money"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("calculation/{sourse}/{valuein}/to/{money}")]
        public IActionResult GetQuoteCalculationY(string sourse, double valuein, string money)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = _quoteService.CalculationY(sourse, valuein, money);

            if (result == 0)
            {
                return NotFound();
            }

            return Ok(result);
        }

        #endregion
    }
}