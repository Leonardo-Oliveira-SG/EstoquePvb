using Application.DTOs;

namespace Application.Interfaces
{
    public interface IEntradaRoloService
    {
        Task<EntradaRoloDto>IncrementarEstoque(EntradaRoloDto entradaRoloDto);
        Task<SaidaRoloDto> BaixarEstoque(SaidaRoloDto saidaRoloDto);
    }
}
