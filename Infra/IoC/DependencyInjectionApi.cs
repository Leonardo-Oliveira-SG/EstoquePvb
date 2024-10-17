using Infra.Context;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Application.Interfaces;
using Application.Interfaces.Queries;
using Application.Services;
using Domain.Interfaces;
using Infra.Queries;
using Infra.Repositories;
using System.Diagnostics.CodeAnalysis;


namespace Infra.IoC
{
    [ExcludeFromCodeCoverage]
    public static class DependencyInjectionApi
    {
        public static IServiceCollection AddInfrastructureApi(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ApplicationDbContext>();

            #region Services
            
            services.AddScoped<IPvbService, PvbService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IEntradaRoloService, ControleEstoquePvbService>();
            services.AddScoped<IBuscaService, BuscaService>();
            #endregion

            #region Queries

            services.AddScoped<IPvbQuery, PvbQuery>();
            #endregion

            #region Repository
            
            services.AddScoped<IPvbRepository, PvbRepository>();
            services.AddScoped<IEstoqueFabricanteRepository, EstoqueFabricanteRepository>();
            services.AddScoped<IEstoquePvbRepository, EstoquePvbRepository>();
            services.AddScoped<IEstoqueTemporarioRepository, EstoqueTemporarioRepository>();
            services.AddScoped<IMovimentacaoPvbRepository, MovimentacaoPvbRepository>();
            #endregion

            return services;
        }
    }
}
