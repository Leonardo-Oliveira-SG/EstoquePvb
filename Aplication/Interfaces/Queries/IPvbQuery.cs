using Application.DTOs;
using Domain.Entities;

namespace Application.Interfaces.Queries
{
    public interface IPvbQuery
    {
        Task<List<Pvb?>?> Get();
        Task<PaginationResponse<PvbDto>> GetFilter(PaginationRequest paginationRequest);
        Task<PvbDto> GetById(int Codigo);
    }
}