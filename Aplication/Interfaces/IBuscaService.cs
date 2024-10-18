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
        Task<List<CoberturaDeEstoqueDto>> GetBuscaCoberturaDeEstoquePorEspessuraAsync(decimal espessura);
        Task<List<CoberturaDeEstoqueDto>> GetBuscaCoberturaDeEstoquePorDestinoAsync(string destino);
        Task<List<CoberturaDeEstoqueDto>> GetBuscaCoberturaDeEstoquePorEspessuraAndDestinoAsync(decimal espessura, string destino);

    }
}
