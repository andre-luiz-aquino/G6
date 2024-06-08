using AutoMapper;
using G6.Application.Interfaces;
using G6.Application.ViewModels;
using G6.Domain.Entities;
using G6.Domain.Interfaces;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G6.Application.Services
{
    public class AtivosService : IAtivosService
    {
        private readonly IAtivosRepository _repository;
        private readonly IMapper _mapper;
        private readonly IDadosHistoricosRepository _dadosHistoricosRepository;
        private const string BEARER_TOKEN = "3VVNdzsVGoP97zz6JtJuZA";

        public AtivosService(IAtivosRepository repository, IMapper mapper, IDadosHistoricosRepository dadosHistoricosRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _dadosHistoricosRepository = dadosHistoricosRepository;
        }


        public async Task AtivoQuoteTickers(BrapiTickerRequestViewModel requestVM, bool interno = false)
        {
            var client = new RestClient("https://brapi.dev");
            var request = new RestRequest($"/api/quote/{requestVM.Ativo}?range={requestVM.Range}&interval={requestVM.Interval}", Method.Get);

            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", $"Bearer {BEARER_TOKEN}");

            var response = await client.ExecuteAsync(request);

            var responseObject = JsonConvert.DeserializeObject<AtivosResponseListViewModel>(value: response.Content);

            var ativoDB = await _repository.GetAtivoByNome(requestVM.Ativo);

            var list = new List<Ativos>() { ativoDB };

            if (!interno)
            {
                var updateAtivo = from dados in responseObject.results
                                  join ativo in list on dados.Symbol equals ativo.Symbol
                                  select new Ativos
                                  {
                                      Currency = dados.Currency,
                                      LogoUrl = dados.LogoUrl,
                                      Symbol = ativo.Symbol,
                                      averageDailyVolume3Month = dados.averageDailyVolume3Month,
                                      averageDailyVolume10Day = dados.averageDailyVolume10Day,
                                      longName = dados.longName
                                  };

                foreach (var ativo in updateAtivo)
                    await _repository.AtualizarAtivo(ativo);
            }
            
            List<DadosHistoricosAtivos> dadosHistoricos = new List<DadosHistoricosAtivos>();
            List<DadosHistoricosAtivos> dadosHistoricosInsert = new List<DadosHistoricosAtivos>();

            foreach (var ativo in responseObject.results)
                foreach (var dado in ativo.historicalDataPrice)
                {
                    dadosHistoricos.Add(dado);
                }

            if(interno)
            {
                foreach (var dados in dadosHistoricos)
                {
                    DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(long.Parse(dados.Date));
                    DateTime dateTime = dateTimeOffset.UtcDateTime;

                    if(dateTime.Day == DateTime.UtcNow.Day)
                    {
                        var dadoInsert = new DadosHistoricosAtivos()
                        {
                            Date = dados.Date,
                            Open = dados.Open,
                            High = dados.High,
                            Low = dados.Low,
                            Close = dados.Close,
                            AdjustedClose = dados.AdjustedClose,
                            Volume = dados.Volume,
                            AtivosId = ativoDB.Id
                        };

                        dadosHistoricosInsert.Add(dadoInsert);
                    }
                }
            }
            else
            {
                foreach (var dados in dadosHistoricos)
                {
                    var dadoInsert = new DadosHistoricosAtivos()
                    {
                        Date = dados.Date,
                        Open = dados.Open,
                        High = dados.High,
                        Low = dados.Low,
                        Close = dados.Close,
                        AdjustedClose = dados.AdjustedClose,
                        Volume = dados.Volume,
                        AtivosId = ativoDB.Id
                    };

                    dadosHistoricosInsert.Add(dadoInsert);
                }
            }

            await _dadosHistoricosRepository.InsertDadosHistoricos(dadosHistoricosInsert);
        }

        public async Task<IEnumerable<string>> AtivoQuoteList()
        {
            var client = new RestClient("https://brapi.dev");
            var request = new RestRequest("/api/quote/list", Method.Get);

            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", $"Bearer {BEARER_TOKEN}");

            var response = await client.ExecuteAsync(request);

            var vm = JsonConvert.DeserializeObject<AtivoStockList>(value: response.Content);

            var query = vm?.stocks.OrderByDescending(x => x.volume).Take(50).Select(e => e.stock);

            var insert = await _repository.InsertAtivosList(query);

            if (!insert)
                return null;

            return query;
        }

        public async Task AtualizarTodosAtivo(BrapiTickerRequestViewModelTodos requestVM)
        {
            var ativos = await _repository.GetAtivoCodigos();

            foreach (var ativo in ativos)
            {
                var client = new RestClient("https://brapi.dev");
                var request = new RestRequest($"/api/quote/{ativo}?range={requestVM.Range}&interval={requestVM.Interval}", Method.Get);

                request.AddHeader("Accept", "application/json");
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Authorization", $"Bearer {BEARER_TOKEN}");

                var response = await client.ExecuteAsync(request);

                var responseObject = JsonConvert.DeserializeObject<AtivosResponseListViewModel>(value: response.Content);

                var ativoDB = await _repository.GetAtivoByNome(ativo);

                var list = new List<Ativos>() { ativoDB };

                var updateAtivo = from dados in responseObject.results
                                  join at in list on dados.Symbol equals at.Symbol
                                  select new Ativos
                                  {
                                      Currency = dados.Currency,
                                      LogoUrl = dados.LogoUrl,
                                      Symbol = at.Symbol,
                                      averageDailyVolume3Month = dados.averageDailyVolume3Month,
                                      averageDailyVolume10Day = dados.averageDailyVolume10Day,
                                      longName = dados.longName
                                  };

                foreach (var at in updateAtivo)
                    await _repository.AtualizarAtivo(at);

                List<DadosHistoricosAtivos> dadosHistoricos = new List<DadosHistoricosAtivos>();
                List<DadosHistoricosAtivos> dadosHistoricosInsert = new List<DadosHistoricosAtivos>();

                foreach (var at in responseObject.results)
                    foreach (var dado in at.historicalDataPrice)
                    {
                        dadosHistoricos.Add(dado);
                    }

                foreach (var dados in dadosHistoricos)
                {
                    var dadoInsert = new DadosHistoricosAtivos()
                    {
                        Date = dados.Date,
                        Open = dados.Open,
                        High = dados.High,
                        Low = dados.Low,
                        Close = dados.Close,
                        AdjustedClose = dados.AdjustedClose,
                        Volume = dados.Volume,
                        AtivosId = ativoDB.Id
                    };
                    dadosHistoricosInsert.Add(dadoInsert);
                }
                await _dadosHistoricosRepository.InsertDadosHistoricos(dadosHistoricosInsert);
            }
        }

        public async Task<ListaMelhoresAtivos> GetFuncaoMelhoresAtivos(string nome)
        {
            var melhoresAtivos = await _repository.GetFuncaoMelhoresAtivos(nome);
            
            if (melhoresAtivos is not null)
                return melhoresAtivos;

            return null;
        }


        public async Task<List<DadosHistoricosAtivos>> GetHistoricoAtivo(string ativo)
        {
           
            var historicoAtivo = await _dadosHistoricosRepository.GetHistoricoAtivo(ativo);

            foreach (var item in historicoAtivo)
            {
                DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(long.Parse(item.Date));
                item.Date = dateTimeOffset.UtcDateTime.ToString("yyyy-MM-dd");
            }
               
            return historicoAtivo;
        }


        public async Task<ListaTop10Ativos> GetTop10Ativos()
        {
            var top10Ativos = await _repository.GetTop10Ativos();

            if (top10Ativos is not null)
                return top10Ativos;

            return null;
        }

        public async Task<RetornoRelatorioAtivo> GetRelatorioAtivo(int ativoId, bool paridadeRiscos)
        {
            var relatorioAtivo = await _repository.GetRelatorioPorAtivo(ativoId, paridadeRiscos);


            if (relatorioAtivo is not null)
                return relatorioAtivo;

            return null;
        }

        public async Task<RetornoRendimentoTotalCarteira> GetRendimentoTotalCarteira(bool paridadeRiscos)
        {
            var rendimentoTotalCarteira = await _repository.GetRendimentoTotalCarteira(paridadeRiscos);

            if (rendimentoTotalCarteira is not null)
                return rendimentoTotalCarteira;

            return null;
        }

        public async Task<RetornoRelatorioTodosAtivos> GetRelatorioTodosAtivos(bool paridadeRiscos)
        {
            var relatorioTodosAtivos = await _repository.GetRelatorioAllAtivos(paridadeRiscos);

            if (relatorioTodosAtivos is not null)
                return relatorioTodosAtivos;

            return null;
        }

        public async Task<RetornoRelatorioRetornoDiarioCarteira> GetRendimentoDiarioCarteira(bool paridadeRiscos)
        {
            var relatorioRendimentoDiario = await _repository.GetRelatorioDiarioCarteira(paridadeRiscos);

            if (relatorioRendimentoDiario is not null)
                return relatorioRendimentoDiario;

            return null;
        }
    }
}
