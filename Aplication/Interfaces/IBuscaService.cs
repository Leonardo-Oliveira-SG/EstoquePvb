using Application.DTOs;

namespace Application.Interfaces
{
    public interface IBuscaService
    {
        Task<PaginationResponse<BuscaMovimentacaoPvbDto>> GetBuscaMovimentacaoPvbAsync(PaginationRequest paginationRequest);
        Task<List<BuscaEstoqueDto>> GetBuscaEstoqueAsync();
        Task<List<BuscaEstoqueFabricanteDto>> GetBuscaEstoqueFabricanteAsync();
        Task<List<BuscaEstoqueTemporarioDto>> GetBuscaEstoqueTemporarioAsync();
        Task<List<CoberturaDeEstoqueDto>> GetBuscaCoberturaDeEstoqueAsync();
    }
}
