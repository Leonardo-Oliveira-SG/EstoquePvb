using Application.DTOs;
using Application.Interfaces.Queries;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infra.Queries
{
    internal class PvbQuery : IPvbQuery
    {
        //private readonly ApplicationDbContext _dbContext;
        private readonly IPvbRepository _pvbRepository;
        private readonly IEstoquePvbRepository _estoquePvbRepository;

        public PvbQuery(/*ApplicationDbContext dbContext,*/ IEstoquePvbRepository estoquePvbRepository, IPvbRepository pvbRepository)
        {
            //_dbContext = dbContext;
            _pvbRepository = pvbRepository;
            _estoquePvbRepository = estoquePvbRepository;
        }

        public async Task<List<PvbDto?>?> Get()
        {
            var pvbs = _pvbRepository.Pvb.AsNoTracking().ToList();

            if (pvbs == null || !pvbs.Any())
            {
                return null;
            }

            var pvbsDto = pvbs.Select(pvb => new PvbDto
            {
                Codigo = pvb.Codigo,
                CodigoPvb = pvb.CodigoPvb,
                Fabricante = pvb.Fabricante,
                TipoPvb = pvb.TipoPvb,
                Espessura = pvb.Espessura,
                TamanhoRolo = pvb.TamanhoRolo,
            }).ToList();

            return pvbsDto!;
        }


        public async Task<PaginationResponse<PvbDto>> GetFilter(PaginationRequest paginationRequest)
        {
            var query = _pvbRepository.Pvb.AsNoTracking();

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
            var pvb = await _pvbRepository.Pvb
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

        public async Task<List<PvbDto?>?> GetPvbEmEstoque()
        {
            var pvb = from p in _pvbRepository.Pvb
                      join e in _estoquePvbRepository.EstoquePvb on p.Codigo equals e.Codigo
                      where e.Saldo > 0
                      select new PvbDto
                      {
                          Codigo = p.Codigo,
                          TipoPvb = p.TipoPvb,
                          CodigoPvb = p.CodigoPvb,
                          Fabricante = p.Fabricante,
                          Espessura = p.Espessura,
                          TamanhoRolo = p.TamanhoRolo,
                      };

            var listaPvb = await pvb.ToListAsync();

            if (listaPvb == null)
            {
                return null;
            }

            return listaPvb;
        }
    }
}
