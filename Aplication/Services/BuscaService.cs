using Application.DTOs;
using Application.Interfaces;
using Application.Interfaces.Queries;
using Domain.Interfaces;

namespace Application.Services
{
    public class BuscaService : IBuscaService
    {
        private readonly IEstoqueTemporarioRepository _estoqueTemporarioRepository;
        private readonly IPvbRepository _pvbRepository;
        private readonly IEstoqueFabricanteRepository _estoqueFabricanteRepository;
        private readonly IMovimentacaoPvbRepository _movimentacaoRepository;
        private readonly IEstoquePvbRepository _estoquePvbRepository;
        private readonly IBuscaQuery _buscaQuery;

        public BuscaService(
            IEstoqueTemporarioRepository estoqueTemporarioRepository,
            IEstoquePvbRepository estoquePvbRepository,
            IPvbRepository pvbRepository,
            IEstoqueFabricanteRepository estoqueFabricanteRepository,
            IMovimentacaoPvbRepository movimentacaoRepository,
            IBuscaQuery buscaQuery)
        {
            _estoqueTemporarioRepository = estoqueTemporarioRepository;
            _estoquePvbRepository = estoquePvbRepository;
            _pvbRepository = pvbRepository;
            _estoqueFabricanteRepository = estoqueFabricanteRepository;
            _movimentacaoRepository = movimentacaoRepository;
            _buscaQuery = buscaQuery;
        }

        public async Task<PaginationResponse<BuscaMovimentacaoPvbDto>> GetBuscaMovimentacaoPvbAsync(PaginationRequest paginationRequest)
        {
            return await _buscaQuery.GetBuscaMovimentacaoPvbAsync(paginationRequest);
        }
        public async Task<List<BuscaEstoqueDto>> GetBuscaEstoqueAsync()
        {
            return await _buscaQuery.GetBuscaEstoqueAsync();
        }
        public async Task<List<BuscaEstoqueFabricanteDto>> GetBuscaEstoqueFabricanteAsync()
        {
            return await _buscaQuery.GetBuscaEstoqueFabricanteAsync();
        }
        public async Task<List<BuscaEstoqueTemporarioDto>> GetBuscaEstoqueTemporarioAsync()
        {
            return await _buscaQuery.GetBuscaEstoqueTemporarioAsync();
        }
        public async Task<List<CoberturaDeEstoqueDto>> GetBuscaCoberturaDeEstoqueAsync()
        {
            return await _buscaQuery.GetBuscaCoberturaDeEstoqueAsync();
        }
        public async Task<List<CoberturaDeEstoqueDto>> GetBuscaCoberturaDeEstoquePorEspessuraAsync(decimal espessura)
        {
            return await _buscaQuery.GetBuscaCoberturaDeEstoquePorEspessuraAsync(espessura);
        }
        public async Task<List<CoberturaDeEstoqueDto>> GetBuscaCoberturaDeEstoquePorDestinoAsync(string destino)
        {
            return await _buscaQuery.GetBuscaCoberturaDeEstoquePorDestinoAsync(destino);
        }
        public async Task<List<CoberturaDeEstoqueDto>> GetBuscaCoberturaDeEstoquePorEspessuraAndDestinoAsync(decimal espessura, string destino)
        {
            return await _buscaQuery.GetBuscaCoberturaDeEstoquePorEspessuraAndDestinoAsync(espessura, destino);
        }
    }
}
