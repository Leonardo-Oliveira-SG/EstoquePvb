using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Configuration
{
    public class EstoqueFabricanteConfiguration : IEntityTypeConfiguration<EstoqueFabricante>
    {
        public void Configure(EntityTypeBuilder<EstoqueFabricante> builder)
        {
            // Definir o nome da tabela no banco de dados
            builder.ToTable("Tb_Estoquefabricante");

            // Chave primária
            builder.HasKey(ef => ef.Id);
            builder.Property(ef => ef.Id).HasColumnName("Id").IsRequired();

            // Definir as propriedades da tabela
            builder.Property(ef => ef.Saldo).IsRequired();
            builder.Property(ef => ef.Codigo).IsRequired();
            builder.Property(ef => ef.Fabricante).IsRequired().HasMaxLength(50);
        }
    }
}