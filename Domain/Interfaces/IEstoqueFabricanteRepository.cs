using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IEstoqueFabricanteRepository
    {        
        IQueryable<EstoqueFabricante> EstoqueFabricante { get; }

        Task AddAsync(EstoqueFabricante estoqueFabricante);
        Task Update(EstoqueFabricante estoqueFabricante);
        Task<bool> DeleteByFabricanteAndCodigo(string Fabricante, int Codigo);
        Task SaveChangesAsync();
        Task<EstoqueFabricante> GetByFabricanteAndCodigo(string Fabricante, int Codigo);
        Task RemoveAsync(EstoqueFabricante estoqueFabricante);
    }
}
