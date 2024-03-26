using G6.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G6.Persistence.Mapping
{
    public class AtivosMapping : IEntityTypeConfiguration<Ativos>
    {
        public void Configure(EntityTypeBuilder<Ativos> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.averageDailyVolume3Month).HasColumnType("decimal(18, 2)");
            builder.Property(x => x.averageDailyVolume10Day).HasColumnType("decimal(18, 2)");

            // Define o relacionamento com DadosHistoricosAtivos
            builder.HasMany(x => x.DadosHistoricosAtivos)
                   .WithOne(y => y.Ativos)
                   .HasForeignKey(y => y.AtivosId)
                   .OnDelete(DeleteBehavior.Cascade); // Isso define a ação de exclusão em cascata, se necessário
        }
    }
}
