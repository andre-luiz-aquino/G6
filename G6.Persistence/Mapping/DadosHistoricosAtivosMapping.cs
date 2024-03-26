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
    public class DadosHistoricosAtivosMapping : IEntityTypeConfiguration<DadosHistoricosAtivos>
    {
        public void Configure(EntityTypeBuilder<DadosHistoricosAtivos> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Date).IsRequired();
            builder.Property(x => x.Open).HasColumnType("decimal(18, 2)");
            builder.Property(x => x.High).HasColumnType("decimal(18, 2)");
            builder.Property(x => x.Low).HasColumnType("decimal(18, 2)");
            builder.Property(x => x.Close).HasColumnType("decimal(18, 2)");
            builder.Property(x => x.Volume).IsRequired();
            builder.Property(x => x.AdjustedClose).HasColumnType("decimal(18, 2)");

            // Define o relacionamento com Ativos
            builder.HasOne(x => x.Ativos)
                   .WithMany(y => y.DadosHistoricosAtivos)
                   .HasForeignKey(x => x.AtivosId)
                   .IsRequired();
        }
    }
}
