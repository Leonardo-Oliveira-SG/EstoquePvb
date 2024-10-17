using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Configuration
{
    public class EstoquePvbConfiguration : IEntityTypeConfiguration<EstoquePvb>
    {
        public void Configure(EntityTypeBuilder<EstoquePvb> builder)
        {
            // Definir o nome da tabela no banco de dados
            builder.ToTable("Tb_EstoquePvb");

            // Chave primária
            builder.HasKey(ep => ep.Codigo);
            builder.Property(ep => ep.Codigo).HasColumnName("Codigo").IsRequired();

            // Definir as propriedades da tabela
            builder.Property(ep => ep.Saldo).IsRequired();
        }
    }
}
