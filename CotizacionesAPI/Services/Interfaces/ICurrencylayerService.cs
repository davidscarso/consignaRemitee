using CotizacionesAPI.Models;
using System.Threading.Tasks;

namespace CotizacionesAPI.Services
{
    public interface ICurrencylayerService
    {
        Task<CurrencylayerModel> GetQuotes();

        Task<int> UpdateQuotesToBaseAsync();

    }
}
