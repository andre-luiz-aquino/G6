using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G6.Domain.Entities
{
    public class RendimentoTotalCarteira
    {
        public DateOnly dataHora { get; set; }
        public int carteiraId { get; set; }
        public double rendimento {  get; set; }
        public double investimento { get; set; }
    }
}
