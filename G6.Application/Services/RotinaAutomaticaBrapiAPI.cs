
using G6.Application.Interfaces;
using G6.Application.ViewModels;
using G6.Domain.Entities;
using G6.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G6.Application.Services
{
    public class RotinaAutomaticaBrapiAPI : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public RotinaAutomaticaBrapiAPI(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while(!stoppingToken.IsCancellationRequested)
            {
                if(DateTime.UtcNow.AddHours(-3).Hour == 20)
                {
                    var listaAcionamentoViewModel = new List<BrapiTickerRequestViewModel>();
                    var listaAtivos = new List<Ativos>();

                    using (var scope = _scopeFactory.CreateScope())
                    {
                        var _ativosRepository = scope.ServiceProvider.GetRequiredService<IAtivosRepository>();
                        listaAtivos.AddRange(await _ativosRepository.GetTodosAtivos());

                    }

                    using (var scope = _scopeFactory.CreateScope())
                    {
                        var _ativosService = scope.ServiceProvider.GetRequiredService<IAtivosService>();

                        foreach (var item in listaAtivos)
                        {
                            var objetoAcionamento = new BrapiTickerRequestViewModel()
                            {
                                Ativo = item.Symbol,
                                Range = "1d",
                                Interval = "1d"
                            };

                            listaAcionamentoViewModel.Add(objetoAcionamento);
                        }

                        foreach (var item in listaAcionamentoViewModel)
                        {
                            await _ativosService.AtivoQuoteTickers(item, true);
                        }
                    }
                }
                await Task.Delay(TimeSpan.FromSeconds(3600));
            }
        }
    }
}
