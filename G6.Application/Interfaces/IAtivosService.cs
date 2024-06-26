﻿using G6.Application.ViewModels;
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
        Task<RetornoRelatorioAtivo> GetRelatorioAtivo(int ativoId, bool paridadeRiscos);
        Task<ListaTop10Ativos> GetTop10Ativos(bool paridadeRiscos);
        Task<RetornoRendimentoTotalCarteira> GetRendimentoTotalCarteira(bool paridadeRiscos);
        Task<RetornoRelatorioTodosAtivos> GetRelatorioTodosAtivos(bool paridadeRiscos);
        Task<RetornoRelatorioRetornoDiarioCarteira> GetRendimentoDiarioCarteira(bool paridadeRiscos);
        Task<RetornoTelaInicial> GetContextoTelaInicial(bool paridadeRiscos);
    }
}
