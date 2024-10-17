using Domain.Entities;
using Infra.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infra.Context
{
    public class ApplicationDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public ApplicationDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = _configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        
        public DbSet<Pvb> Pvb { get; set; }
        public DbSet<EstoquePvb> EstoquePvb { get; set; }
        public DbSet<EstoqueTemporario> EstoqueTemporario { get; set; }
        public DbSet<MovimentacaoPvb> MovimentacaoPvb { get; set; }
        public DbSet<EstoqueFabricante> EstoqueFabricante { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            

            modelBuilder.ApplyConfiguration(new PvbConfiguration());
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new EstoquePvbConfiguration());
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new EstoqueTemporarioConfiguration());
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new MovimentacaoPvbConfiguration());
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new EstoqueFabricanteConfiguration());
            base.OnModelCreating(modelBuilder);
        }

        

    }
}
