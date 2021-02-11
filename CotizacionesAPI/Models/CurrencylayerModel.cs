using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CotizacionesAPI.Models
{
    /// <summary>
    /// This model is used to bring quotes from Currencylayer
    /// /// </summary>
    public class CurrencylayerModel
    {
        public bool Success { get; set; }
        public string Terms { get; set; }
        public string Privacy { get; set; }
        public string Timestamp { get; set; }
        public string Source { get; set; }
        public Dictionary<string, double> Quotes { get; set; }
    }

}
