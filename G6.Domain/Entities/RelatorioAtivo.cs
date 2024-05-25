using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G6.Domain.Entities
{
    public class RelatorioAtivo
    {
        public int ativoId { get; set; }
        public double pesoAcao { get; set; }
        public double investimento { get; set; }
        public double renda { get; set; }
        public DateOnly dataHora { get; set; }
        public int carteiraId { get; set; }
    }
}
