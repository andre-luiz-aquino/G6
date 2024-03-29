using G6.Domain.Entities;
using G6.Persistence.Mapping;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace G6.Persistence.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Ativos>? Ativos { get; set; }
        public DbSet<DadosHistoricosAtivos> DadosHistoricosAtivos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AtivosMapping());
            modelBuilder.ApplyConfiguration(new DadosHistoricosAtivosMapping());
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema("Ativos");
        }
    }
}
