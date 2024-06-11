using AutoMapper;
using G6.Application.Interfaces;
using G6.Application.ViewModels;
using G6.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace G6.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AtivosController : Controller
    {
        private readonly IAtivosService _ativosService;
        private readonly IMapper _mapper;

        public AtivosController(IAtivosService ativosService, IMapper mapper)
        {
            _ativosService = ativosService;
            _mapper = mapper;
        }


        [HttpPost]
        public async Task<ActionResult> RequestQuoteTickersBrapi([FromQuery] BrapiTickerRequestViewModel request)
        {
            await _ativosService.AtivoQuoteTickers(request, false);
            return Ok();
        }

        [HttpPost, Route("list")]
        public async Task<ActionResult> RequestQuoteListBrapi()
        {
            return Ok(await _ativosService.AtivoQuoteList());
        }

        [HttpPost, Route("todos")]
        public async Task<ActionResult> AtualizarTodosAtivo([FromQuery] BrapiTickerRequestViewModelTodos request)
        {
            await _ativosService.AtualizarTodosAtivo(request);
            return Ok();
        }
        [HttpPost, Route("montar-carteira")]
        public async Task<ActionResult<List<ListaMelhoresAtivos>>> BuscarMelhoresAtivos([FromBody] MontarCarteiraRequestViewModel request)
        {
            var melhoresAtivos = await _ativosService.GetFuncaoMelhoresAtivos(request.nomeCarteira);
            return Ok(melhoresAtivos);
        }

        [HttpGet, Route("buscar-historico-ativos")]
        public async Task<ActionResult<List<DadosHistoricosAtivos>>> GetHistoricoAtivo([FromQuery] string Ativo)
        {
            var historicoAtivo = await _ativosService.GetHistoricoAtivo(Ativo);
            return Ok(historicoAtivo);
        }
        [HttpGet, Route("get-ativos")]
        public async Task<ActionResult<List<ListaTop10Ativos>>> GetTop10Ativos([FromQuery] bool paridadeRiscos)
        {
            var top10Ativos = await _ativosService.GetTop10Ativos(paridadeRiscos);

            if (top10Ativos is null)
                return BadRequest();

            return Ok(top10Ativos);
        }

        [HttpGet, Route("relatorio-por-ativo")]
        public async Task<ActionResult<List<RetornoRelatorioAtivo>>> GetRelatorioPorAtivo([FromQuery] int ativoId, bool paridadeRiscos)
        {
            var relatorioAtivo = await _ativosService.GetRelatorioAtivo(ativoId, paridadeRiscos);

            if (relatorioAtivo is null)
                return BadRequest();

            return Ok(relatorioAtivo);
        }

        [HttpGet("relatorio-todos-ativos")]
        public async Task<ActionResult<List<RetornoRelatorioTodosAtivos>>> GetRelatorioTodosAtivos([FromQuery] bool paridadeRiscos)
        {
            var relatotoTodosAtivos = await _ativosService.GetRelatorioTodosAtivos(paridadeRiscos);

            if (relatotoTodosAtivos is null)
                return BadRequest();

            return Ok(relatotoTodosAtivos);

        }

        [HttpGet, Route("rendimento-total-carteira")]
        public async Task<ActionResult<List<RetornoRelatorioAtivo>>> GetRelatorioPorAtivo([FromQuery] bool paridadeRiscos)
        {
            var rendimentoTotal = await _ativosService.GetRendimentoTotalCarteira(paridadeRiscos);

            if (rendimentoTotal is null)
                return BadRequest();

            return Ok(rendimentoTotal);
        }

        [HttpGet("rendimento-diario-carteira")]
        public async Task<ActionResult<List<RetornoRelatorioRetornoDiarioCarteira>>> GetRelatorioDiario([FromQuery] bool paridadeRiscos)
        {
            var rendimentoDiario = await _ativosService.GetRendimentoDiarioCarteira(paridadeRiscos);

            if (rendimentoDiario is null)
                return BadRequest();

            return Ok(rendimentoDiario);
        }

        [HttpGet("contexto-tela-inicial")]
        public async Task<ActionResult<List<RetornoTelaInicial>>> GetContextoTelaInicial([FromQuery] bool paridadeRiscos)
        {
            var contextoTelaInicial = await _ativosService.GetContextoTelaInicial(paridadeRiscos);

            if (contextoTelaInicial is null)
                return BadRequest();

            return Ok(contextoTelaInicial);
        }
    }
}
