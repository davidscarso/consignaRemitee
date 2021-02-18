using CotizacionesAPI.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace CotizacionesAPI.Services
{
    public class CurrencylayerService : ICurrencylayerService
    {
        // set endpoint and your access key
        private const string _endpoint = "live";
        private const string _access_key = "f84a3f45e808ce991fef73a5fefa16be";
        private const string _url = "http://api.currencylayer.com/";

        private const string _urlAPI = _url + _endpoint + "?access_key=" + _access_key + "&format=1";

        private readonly DataContext _context;
        private readonly QuoteService _quoteService;

        /// <summary>
        /// Constructor
        /// </summary>
        public CurrencylayerService()
        {
            _context = new DataContext();
            _quoteService = new QuoteService();
        }


        /// <summary>
        /// Return Quotres consuming Currencylayer API.
        /// </summary>
        /// <returns>Currencylayer</returns>
        public async Task<CurrencylayerModel> GetQuotes()
        {
            using (var httpClient = new HttpClient())
            {
                CurrencylayerModel currencylayer = new CurrencylayerModel();

                using (var response = await httpClient.GetAsync(_urlAPI))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    currencylayer = JsonConvert.DeserializeObject<CurrencylayerModel>(apiResponse);

                }

                return currencylayer;
            }
        }

        /// <summary>
        /// Update the Data Base with quotes
        /// </summary>
        /// <returns>number of Quotes</returns>
        public async Task<int> UpdateQuotesToBaseAsync()
        {

            QuoteService quateService = new QuoteService();
            CurrencylayerModel currencylayer = await GetQuotes();

            foreach (var item in currencylayer.Quotes)
            {
                var newQuote = new QuoteModel
                {
                    Id = item.Key,
                    Value = item.Value
                };


                if (!_quoteService.Exists(newQuote.Id))
                {
                    await _quoteService.PostOne(newQuote);
                }
                else
                {
                    await _quoteService.PutOne(newQuote.Id, newQuote);
                }


                //if (!_context.Quotes.Any(e => e.Id == newQuote.Id))
                //{
                //    _context.Quotes.Add(newQuote);
                //    await _context.SaveChangesAsync();
                //}
                //else
                //{
                //    _context.Entry(newQuote).State = EntityState.Modified;
                //    await _context.SaveChangesAsync();
                //}
            }

            return currencylayer.Quotes.Count;

        }

    }
}

