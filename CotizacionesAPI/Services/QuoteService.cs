using CotizacionesAPI.Models;
using CotizacionesAPI.Services.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CotizacionesAPI.Services
{
    public class QuoteService : IQuoteService
    {
        private readonly DataContext _context;

        /// <summary>
        /// % of FEE
        /// </summary>
        private const double _fee = 1.5; // << set % of FEE
        private const string _sourse = "USD";

        public QuoteService()
        {
            _context = new DataContext();
        }


        public async Task<IEnumerable<QuoteModel>> GetAll()
        {
            var result = new List<QuoteModel>();
            try
            {
                result = await _context.Quotes.ToListAsync();

            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }


        public async Task<QuoteModel> GetOne(string id)
        {
            var result = new QuoteModel();
            try
            {
                result = await _context.Quotes.FindAsync(id);

            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }


        public async Task<int> PutOne(string id, QuoteModel quoteModel)
        {
            try
            {
                _context.Entry(quoteModel).State = EntityState.Modified;

                return await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Por aca pasara la excepcion DbUpdateConcurrencyException:
                // Database operation expected to affect 1 row(s) but actually affected 0 row(s). Data may have been modified or deleted since entities were loaded. See http://go.microsoft.com/fwlink/?LinkId=527962 for information on understanding and handling optimistic concurrency exceptions.

                throw;
            }
        }


        public async Task<QuoteModel> PostOne(QuoteModel quoteModel)
        {
            try
            {
                _context.Quotes.Add(quoteModel);

                await _context.SaveChangesAsync();

                return await GetOne(quoteModel.Id);
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<int> DeleteOne(string id)
        {
            try
            {
                var quoteModel = await _context.Quotes.FindAsync(id);
                if (quoteModel != null)
                {
                    _context.Quotes.Remove(quoteModel);
                    return await _context.SaveChangesAsync();
                }
                return 0;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public bool Exists(string id)
        {
            return _context.Quotes.Any(e => e.Id == id);
        }


        //a) Calcular el monto X a enviar en moneda M para que llegue el monto Y en moneda N.
        //b) Calcular el monto Y a recibir en moneda N enviando el monto X en moneda M.
        //Ej: ¿Si quiero que lleguen 100 BOB cuantos ARS debo enviar?
        //Ej: ¿Si envió 10.000 ARS cuántos BOB llegarían?.
        //-En el cálculo se debe tener en cuenta el fee que es un % del monto enviado.
        //-En el cálculo se debe tener en cuenta el tipo de cambio almacenado en la base de datos.

        /// <summary>
        /// a) Calcular el monto X a enviar en moneda M para que llegue el monto Y en moneda N.
        /// </summary>
        /// <param name="m"></param>
        /// <param name="y"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public double CalculationX(string m, double y, string n)
        {
            double result = 0;
            try
            {
                var feeValues = (_fee / 100);

                var idM = _sourse + m.ToUpper();
                var idN = _sourse + n.ToUpper();

                var quoteM = GetOne(idM).Result; //USD-BOB 6.913913
                var quoteN = GetOne(idN).Result; //USD-ARS 88.50


                if (quoteM != null && quoteN != null)
                {
                    var yUSD = y / quoteN.Value;

                    var xN = yUSD * quoteM.Value;

                    var xNF = xN + (xN * feeValues);

                    result = xNF;
                }
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }

        /// <summary>
        /// b) Calcular el monto Y a recibir en moneda N enviando el monto X en moneda M.
        /// </summary>
        /// <param name="m">moneda origen</param>
        /// <param name="x">monto enviado</param>
        /// <param name="n">modenda destino</param>
        /// <returns></returns>
        public double CalculationY(string m, double x, string n)
        {
            double result = 0;
            try
            {
                var feeValues = (_fee / 100);

                var idM = _sourse + m.ToUpper();
                var idN = _sourse + n.ToUpper();

                var quoteM = GetOne(idM).Result;
                var quoteN = GetOne(idN).Result;

                if (quoteM != null && quoteN != null)
                {
                    x = x - (x * feeValues); //descuento el fee sobre el monto enviado  10.000 - 10.000*feeValues

                    var xUSD = x / quoteM.Value; //convierto a USD

                    var yN = xUSD * quoteN.Value;  //convierto a moneda N

                    result = yN;
                }
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }
    }
}
