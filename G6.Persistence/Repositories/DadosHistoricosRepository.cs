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
    public class DadosHistoricosRepository : IDadosHistoricosRepository
    {
        private readonly ApplicationDbContext _context;

        public DadosHistoricosRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task InsertDadosHistoricos(List<DadosHistoricosAtivos> dadosHistoricos)
        {
            if (dadosHistoricos.Count == 0)
                return;

            await _context.DadosHistoricosAtivos.AddRangeAsync(dadosHistoricos);
            await _context.SaveChangesAsync();
        }


        public async Task<List<DadosHistoricosAtivos>> GetHistoricoAtivo(string ativo)
        {
            var historicoAtivo = await _context.DadosHistoricosAtivos
                .Where(d => d.Ativos.Symbol == ativo)
                .ToListAsync();

            return historicoAtivo;
        }


    }
}

