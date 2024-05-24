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

        [HttpGet, Route("BuscarHistoricoAtivos")]
        public async Task<ActionResult<List<DadosHistoricosAtivos>>> GetHistoricoAtivo([FromQuery] String Ativo)
        {
            var historicoAtivo = await _ativosService.GetHistoricoAtivo(Ativo);
            return Ok(historicoAtivo);
        }
        [HttpGet, Route("get-ativos")]
        public async Task<ActionResult<List<ListaTop10Ativos>>> GetTop10Ativos()
        {
            var top10Ativos = await _ativosService.GetTop10Ativos();

            if (top10Ativos is null)
                return BadRequest();

            return Ok(top10Ativos);
        }

    }
}
