using System.ComponentModel.DataAnnotations;

namespace CotizacionesAPI.Models
{
    /// <summary>
    /// Quote Model    
    /// </summary>
    public class QuoteModel
    {
        /// <summary>
        /// Id de la tabla de Cotizacion.
        /// Sera de la forma que se obtiene desde http://api.currencylayer.com/
        /// "USDARS"
        /// "USDBOB"
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Valor de la cotizacion. 
        /// De la forma USD-> demas monedas
        ///  es ConcurrencyCheck con lo que verificariamos que no se modifique en varias concurrencias en la base de datos.
        /// </summary>
        [ConcurrencyCheck]
        public double Value { get; set; }
    }
}
