using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G6.Domain.Entities
{
    public class RetornoFuncaoTop10
    {
        public string simbolo { get; set; }
        public string longname { get; set; }
        public int ativoId { get; set; }
        public decimal valorAtivo { get; set; }
        public string urlImagem { get; set; }
        public decimal investimento { get; set; }
        public decimal renda { get; set; }
        public decimal valorizacao { get; set; }
        public DateOnly dataHora { get; set; }
    }
}
