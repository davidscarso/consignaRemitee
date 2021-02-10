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
        private readonly QuoteService _quoteService;

        /// <summary>
        /// Constructor
        /// </summary>
        public QuoteController()
        {
            _quoteService = new QuoteService();
        }

        // GET: api/Quote
        [HttpGet]
        public IEnumerable<QuoteModel> GetQuotes()
        {
            return _quoteService.GetAll();
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
        public async Task<IActionResult> PutQuoteModel([FromRoute] string id, [FromBody] QuoteModel quoteModel)
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

            return Ok(_quoteService.PostOne(quoteModel));

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

    }
}