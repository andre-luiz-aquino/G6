using G6.Application.ViewModels;
using G6.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G6.Application.Interfaces
{
    public interface IAtivosService
    {

        
        Task AtivoQuoteTickers(BrapiTickerRequestViewModel request, bool interno);
        Task<IEnumerable<string>> AtivoQuoteList();
        Task AtualizarTodosAtivo(BrapiTickerRequestViewModelTodos requestVM);
        Task<ListaMelhoresAtivos> GetFuncaoMelhoresAtivos(string nome);
        Task<List<DadosHistoricosAtivos>> GetHistoricoAtivo(string ativo);

    }
}
