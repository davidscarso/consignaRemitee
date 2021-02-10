using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CotizacionesAPI.Models
{
    public class QuoteModel
    {
        /// <summary>
        /// Id de la tabla de Cotizacion.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// /// Sera de la forma que se obtiene desde http://api.currencylayer.com/
        /// "USDARS"
        /// "USDBOB"
        /// </summary>
        public string Money { get; set; }

        /// <summary>
        /// Calor de la cotizacion. de la forma USD-> demas monedas
        /// </summary>
        public decimal Value { get; set; }
    }
}
