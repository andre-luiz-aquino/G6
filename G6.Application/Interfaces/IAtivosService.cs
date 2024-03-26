using G6.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G6.Application.Interfaces
{
    public interface IAtivosService
    {

        
        Task AtivoQuoteTickers(BrapiTickerRequestViewModel request);
        Task<IEnumerable<string>> AtivoQuoteList();

        Task AtualizarTodosAtivo(BrapiTickerRequestViewModelTodos requestVM);
    }
}
