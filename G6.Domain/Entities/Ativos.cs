using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G6.Domain.Entities
{
    public class Ativos
    {
        public int Id { get; set; }
        public string? Currency { get; set; }
        public string? LogoUrl { get; set; }
        public string? Symbol { get; set; }
        public decimal? averageDailyVolume3Month { get; set; }
        public decimal? averageDailyVolume10Day { get; set; }
        public string? longName { get; set; }
        public ICollection<DadosHistoricosAtivos>? DadosHistoricosAtivos { get; set; }

    }
}
