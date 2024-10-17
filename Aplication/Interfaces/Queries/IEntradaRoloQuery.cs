using Application.DTOs;
using Domain.Entities;

namespace Application.Interfaces.Queries
{
    public interface IEntradaRoloQuery
    {
        Task<List<EntradaRoloDto>> Get();
        Task<PaginationResponse<EntradaRoloDto>> GetFilter(PaginationRequest paginationRequest);
        Task<PvbDto> GetById(string numeroRolo);
    }
}
