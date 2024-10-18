using Application.DTOs;
using Domain.Entities;

namespace Application.Interfaces.Queries
{
    public interface IPvbQuery
    {
        Task<List<PvbDto?>?> Get();
        Task<List<PvbDto?>?> GetPvbEmEstoque();
        Task<PaginationResponse<PvbDto>> GetFilter(PaginationRequest paginationRequest);
        Task<PvbDto> GetById(int Codigo);
    }
}