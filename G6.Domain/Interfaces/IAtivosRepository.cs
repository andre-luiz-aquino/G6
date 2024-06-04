using G6.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G6.Domain.Interfaces
{
    public interface IAtivosRepository
    {        
        Task AtualizarAtivo(Ativos ativos);
        Task<bool> InsertAtivosList(IEnumerable<string> listaStock);
        Task AtualizarTodosAtivo(Ativos ativos);
        Task<Ativos> GetAtivoByNome(string nome);
        Task<List<String>> GetAtivoCodigos();
        Task<ListaMelhoresAtivos> GetFuncaoMelhoresAtivos(string nome);
        Task<List<Ativos>> GetTodosAtivos();
        Task<ListaTop10Ativos> GetTop10Ativos();
        Task<RetornoRelatorioAtivo> GetRelatorioPorAtivo(int ativoId, bool paridadeRiscos);
        Task NormalizarDadosTabela();
        Task<RetornoRendimentoTotalCarteira> GetRendimentoTotalCarteira(bool paridade);
        Task<RetornoRelatorioTodosAtivos> GetRelatorioAllAtivos(bool paridadeRiscos);
    }
}
