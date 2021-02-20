using CotizacionesAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CotizacionesAPI.Services
{
    public interface IQuoteService
    {
        Task<IEnumerable<QuoteModel>> GetAll();

        Task<QuoteModel> GetOne(string id);

        Task<int> PutOne(string id, QuoteModel quoteModel);

        Task<QuoteModel> PostOne(QuoteModel quoteModel);

        Task<int> DeleteOne(string id);

        bool Exists(string id);

        double CalculationX(string m, double y, string n);

        double CalculationY(string m, double x, string n);
    }
}
