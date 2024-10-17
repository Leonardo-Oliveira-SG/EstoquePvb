using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Configuration
{
    public class EstoqueTemporarioConfiguration : IEntityTypeConfiguration<EstoqueTemporario>
    {
        public void Configure(EntityTypeBuilder<EstoqueTemporario> builder)
        {
            // Definir o nome da tabela no banco de dados
            builder.ToTable("Tb_EstoqueTemporario");

            // Chave primária
            builder.HasKey(et => et.Id);
            builder.Property(et => et.Id).HasColumnName("Id").IsRequired();

            // Definir as propriedades da tabela
            builder.Property(et => et.Saldo).IsRequired();
            builder.Property(et => et.Codigo).IsRequired();
            builder.Property(et => et.NotaFiscal).IsRequired().HasMaxLength(100);
            builder.Property(et => et.NumeroRolo).IsRequired().HasMaxLength(100);
        }
    }
}
