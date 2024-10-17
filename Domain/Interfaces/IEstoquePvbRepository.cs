using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IEstoquePvbRepository
    {
        IQueryable<EstoquePvb> EstoquePvb { get; }

        Task AddAsync(EstoquePvb estoquePvb);
        Task Update(EstoquePvb estoquePvb);
        Task<bool> DeleteById(Guid Id);
        Task<EstoquePvb> GetByCodigoAsync(int Codigo);
        Task SaveChangesAsync();
        Task<bool> Exists(int Codigo);
        Task RemoveAsync(EstoquePvb estoquePvb);
    }
}