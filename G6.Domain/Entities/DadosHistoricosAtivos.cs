﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G6.Domain.Entities
{
    public class DadosHistoricosAtivos
    {
        public int Id { get; set; }
        public string? Date { get; set; }
        public decimal? Open { get; set; }
        public decimal? High { get; set; }
        public decimal? Low { get; set; }
        public decimal? Close { get; set; }
        public int? Volume { get; set; }
        public decimal? AdjustedClose { get; set; }
        public int AtivosId { get; set; }
        public virtual Ativos? Ativos { get; set; }
    }
}
