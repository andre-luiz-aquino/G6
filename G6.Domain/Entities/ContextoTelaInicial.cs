using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G6.Domain.Entities
{
    public class ContextoTelaInicial
    {
        public DateOnly datahora {  get; set; }
        public int carteiraId { get; set; }
        public int countAtivos { get; set; }
        public decimal valorizacao { get; set; }
        public decimal totalValorAtual { get; set; }
        public decimal totalInvestimento { get; set; }
    }
}
