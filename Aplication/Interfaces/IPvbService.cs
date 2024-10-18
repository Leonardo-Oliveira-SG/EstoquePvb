using Application.DTOs;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IPvbService
    {
        Task<Pvb> Add(AddPvbDto pvbDto);
        Task Update(UpdatePvbDto pvbDto);
        Task<bool> Delete(int codigo);
        Task<List<PvbDto>> Get();
        Task<List<PvbDto>> GetPvbEmEstoque();
        Task<PaginationResponse<PvbDto>> GetFilter(PaginationRequest paginationRequest);
        Task<PvbDto> GetById(int codigo);
    }
}