using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IEstoqueTemporarioRepository
    {
        IQueryable<EstoqueTemporario> EstoqueTemporario { get; }

        Task AddAsync(EstoqueTemporario estoqueTemporario);
        Task Update(EstoqueTemporario estoqueTemporario);
        Task<bool> DeleteById(Guid Id);
        Task SaveChangesAsync();
        Task<bool> Exists(string NumeroRolo);
        Task<EstoqueTemporario?> GetByNumeroRoloAsync(string NumeroRolo);
        Task RemoveAsync(EstoqueTemporario estoqueTemporario);
    }
}