using Application.DTOs;
using Application.Interfaces.Queries;
using Domain.Entities;
using Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace Infra.Queries
{
    internal class PvbQuery : IPvbQuery
    {
        private readonly ApplicationDbContext _dbContext;

        public PvbQuery(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Pvb?>?> Get()
        {
            var pvb = _dbContext.Pvb.AsNoTracking().ToList();

            if (pvb == null)
            {
                return null;
            }

            return pvb;
        }

        public async Task<PaginationResponse<PvbDto>> GetFilter(PaginationRequest paginationRequest)
        {
            var query = _dbContext.Pvb.AsNoTracking();

            // Aplicar a paginação antes de carregar os dados
            var paginatedQuery = query
                .Select(p => new PvbDto
                {
                    Codigo = p.Codigo,
                    CodigoPvb = p.CodigoPvb,
                    Fabricante = p.Fabricante,
                    TipoPvb = p.TipoPvb,
                    Espessura = p.Espessura,
                    TamanhoRolo = p.TamanhoRolo
                })
                .Skip((paginationRequest.PageNumber - 1) * paginationRequest.PageSize) // Pule registros anteriores
                .Take(paginationRequest.PageSize); // Pegue apenas PageSize registros

            var data = await paginatedQuery.ToListAsync();

            var totalCount = await query.CountAsync(); // Total de itens sem a paginação

            var paginationResponse = new PaginationResponse<PvbDto>
            {
                PageNumber = paginationRequest.PageNumber,
                PageSize = paginationRequest.PageSize,
                TotalItems = totalCount, // Defina o total de itens sem a paginação
                Data = data,
            };

            return paginationResponse;
        }

        public async Task<PvbDto?> GetById(int codigo)
        {
            var pvb = await _dbContext.Pvb
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Codigo == codigo);

            if (pvb == null)
                return null;

            var pvbDto = new PvbDto
            {
                Codigo = pvb.Codigo,
                CodigoPvb = pvb.CodigoPvb,
                Fabricante = pvb.Fabricante,
                TipoPvb = pvb.TipoPvb,
                Espessura = pvb.Espessura,
                TamanhoRolo = pvb.TamanhoRolo
            };

            return pvbDto;

        }
    }
}
