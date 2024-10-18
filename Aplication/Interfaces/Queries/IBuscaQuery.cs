using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Queries
{
    public interface IBuscaQuery
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
