using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CotizacionesAPI.Models;

namespace CotizacionesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuoteController : ControllerBase
    {
        private readonly DataContext _context;

        /// <summary>
        /// Constructor
        /// </summary>
        public QuoteController()
        {
            _context = new DataContext();
        }

        // GET: api/Quote
        [HttpGet]
        public IEnumerable<QuoteModel> GetQuotes()
        {
            return _context.Quotes;


            //if (_context.Quotes.Any())
            //{
            //    List<QuoteModel> data = _context.Quotes.ToList();
            //    return data;
            //}
            //else
            //{
            //    return null;
            //}

        }

        // GET: api/Quote/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetQuoteModel([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var quoteModel = await _context.Quotes.FindAsync(id);

            if (quoteModel == null)
            {
                return NotFound();
            }

            return Ok(quoteModel);
        }

        // PUT: api/Quote/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuoteModel([FromRoute] int id, [FromBody] QuoteModel quoteModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != quoteModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(quoteModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuoteModelExists(id))
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

            _context.Quotes.Add(quoteModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetQuoteModel", new { id = quoteModel.Id }, quoteModel);
        }

        // DELETE: api/Quote/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuoteModel([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var quoteModel = await _context.Quotes.FindAsync(id);
            if (quoteModel == null)
            {
                return NotFound();
            }

            _context.Quotes.Remove(quoteModel);
            await _context.SaveChangesAsync();

            return Ok(quoteModel);
        }

        private bool QuoteModelExists(int id)
        {
            return _context.Quotes.Any(e => e.Id == id);
        }
    }
}