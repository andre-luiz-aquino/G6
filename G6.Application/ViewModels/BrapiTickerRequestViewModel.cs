using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G6.Application.ViewModels
{
    public class BrapiTickerRequestViewModel
    {
        public string Ativo { get; set; }
        public string? Range { get; set; } = "3mo";
        public string? Interval { get; set; } = "1d";
    }
}
