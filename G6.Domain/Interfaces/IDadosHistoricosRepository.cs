using G6.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G6.Domain.Interfaces
{
    public interface IDadosHistoricosRepository
    {
        Task InsertDadosHistoricos(List<DadosHistoricosAtivos> dadosHistoricos);
    }
}
