using G6.Domain.Entities;
using G6.Domain.Interfaces;
using G6.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G6.Persistence.Repositories
{
    public class AtivosRepository : IAtivosRepository
    {
        private readonly ApplicationDbContext _context;

        public AtivosRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        
        public async Task<bool> InsertAtivosList(IEnumerable<string> listastock)
        {
            List<Ativos> listaInsert = new List<Ativos>();
            try
            {
                foreach (var ativo in listastock)
                {
                    var model = new Ativos()
                    {
                        Symbol = ativo
                    };

                    listaInsert.Add(model);
                }

                await _context.AddRangeAsync(listaInsert);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task AtualizarAtivo(Ativos ativos)
        {
            var ativo = await GetAtivoByNome(ativos.Symbol);

            if (ativos is not null)
            {
                ativo.LogoUrl = ativos.LogoUrl;
                ativo.Symbol = ativos.Symbol;
                ativo.Currency = ativos.Currency;
                ativo.averageDailyVolume10Day = ativos.averageDailyVolume10Day;
                ativo.averageDailyVolume3Month = ativos.averageDailyVolume3Month;
                ativo.longName = ativos.longName;

                _context.Update(ativo);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new ArgumentNullException("Dados invalidos...");
            }
        }
        public async Task AtualizarTodosAtivo(Ativos ativos)
        {

            var listaAtivos = await _context.Ativos.ToListAsync();

            // Verificar se a lista de ativos não está vazia
            if (listaAtivos != null && listaAtivos.Count > 0)
            {
                // Percorrer todos os ativos e atualizar suas informações
                foreach (var ativo in listaAtivos)
                {
                    ativo.LogoUrl = ativos.LogoUrl;
                    ativo.Symbol = ativos.Symbol;
                    ativo.Currency = ativos.Currency;
                    ativo.averageDailyVolume10Day = ativos.averageDailyVolume10Day;
                    ativo.averageDailyVolume3Month = ativos.averageDailyVolume3Month;
                    ativo.longName = ativos.longName;

                    _context.Update(ativo);
                }

                
                await _context.SaveChangesAsync();
            }
            else
            {
                
                throw new Exception("Não há ativos para atualizar.");
            }

        }

      
        public async Task<Ativos> GetAtivoByNome(string nome)
        {
            List<string> list = new List<string>();
            var ativo = await _context.Ativos.FirstOrDefaultAsync(x => x.Symbol == nome);

            if (ativo is not null)
            {
                return ativo;
            }

            return null;
        }

 
        public async Task<List<String>> GetAtivoCodigos()
        {
            var codigos = await _context.Ativos.Select(a => a.Symbol).ToListAsync();
            return codigos;
        }

    }
}
