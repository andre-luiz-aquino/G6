using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G6.Domain.Entities
{
    public class RelatorioTodosAtivos
    {
        public decimal close {  get; set; }
        public int ativoId { get; set; }
        public DateOnly dataHora { get; set; }
        public string urlImagem { get; set; }
    }
}
