using G6.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G6.Application.ViewModels
{
    public class AtivosResponseViewModel
    {
        public int Id { get; set; }
        public string? Currency { get; set; }
        public string? LogoUrl { get; set; }
        public string? Symbol { get; set; }
        public decimal? averageDailyVolume3Month { get; set; }
        public decimal? averageDailyVolume10Day { get; set; }
        public string? longName { get; set; }
        public List<DadosHistoricosAtivos>? historicalDataPrice { get; set; }
    }

    public class AtivosResponseListViewModel
    {
        public List<AtivosResponseViewModel> results { get; set; }
    }
}
