using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Configuration
{
    public class MovimentacaoPvbConfiguration : IEntityTypeConfiguration<MovimentacaoPvb>
    {
        public void Configure(EntityTypeBuilder<MovimentacaoPvb> builder)
        {
            // Definir o nome da tabela no banco de dados
            builder.ToTable("Tb_MovimentacaoPvb");

            // Chave primária
            builder.HasKey(mp => mp.Id);
            builder.Property(mp => mp.Id).HasColumnName("Id").IsRequired();

            // Definir as propriedades da tabela
            builder.Property(mp => mp.Codigo).IsRequired();
            builder.Property(mp => mp.Destino).IsRequired().HasMaxLength(100);
            builder.Property(mp => mp.Data).IsRequired();
            builder.Property(mp => mp.NumeroRolo).IsRequired().HasMaxLength(100);
            
        }
    }
}
