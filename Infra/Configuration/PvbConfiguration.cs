using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Configuration
{
    public class PvbConfiguration : IEntityTypeConfiguration<Pvb>
    {
        public void Configure(EntityTypeBuilder<Pvb> builder)
        {
            // Definir o nome da tabela no banco de dados
            builder.ToTable("Tb_ListaPvb");

            // Chave primária
            builder.HasKey(e => e.Codigo);
            builder.Property(e => e.Codigo).HasColumnName("Codigo").IsRequired();

            // Definir as propriedades da tabela
            builder.Property(e => e.TipoPvb).IsRequired().HasMaxLength(100);
            builder.Property(e => e.CodigoPvb).IsRequired().HasMaxLength(100);
            builder.Property(e => e.Fabricante).IsRequired().HasMaxLength(100);
            builder.Property(e => e.Espessura).IsRequired();
            builder.Property(e => e.TamanhoRolo).IsRequired();
        }
    }
}
