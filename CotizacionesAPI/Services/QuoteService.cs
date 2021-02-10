using CotizacionesAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CotizacionesAPI.Services
{
    public class QuoteService
    {
        private readonly DataContext _context;

        /// <summary>
        /// Constructor
        /// </summary>
        public QuoteService()
        {
            _context = new DataContext();
        }


        public IEnumerable<QuoteModel> GetAll()
        {
            return _context.Quotes;
        }


        public async Task<QuoteModel> GetOne(string id)
        {
            return await _context.Quotes.FindAsync(id);
        }


        public async Task<int> PutOne(string id, QuoteModel quoteModel)
        {
            _context.Entry(quoteModel).State = EntityState.Modified;

            return await _context.SaveChangesAsync();
        }


        public async Task<QuoteModel> PostOne(QuoteModel quoteModel)
        {
            _context.Quotes.Add(quoteModel);

            await _context.SaveChangesAsync();

            return await GetOne(quoteModel.Id);
        }


        public async Task<int> DeleteOne(string id)
        {
            var quoteModel = await _context.Quotes.FindAsync(id);
            if (quoteModel != null)
            {
                _context.Quotes.Remove(quoteModel);
                return await _context.SaveChangesAsync();
                
            }
            return 0;
        }


        public bool Exists(string id)
        {
            return _context.Quotes.Any(e => e.Id == id);
        }


    }
}
