using Domain.Entities;
using System.Threading.Tasks;

namespace Domain.Interfaces
{

    public interface IPvbRepository
    {
        IQueryable<Pvb> Pvb { get; }

        Task AddAsync(Pvb pvb);
        Task Update(Pvb pvb);
        Task<bool> DeleteById(int Codigo);
        Task SaveChangesAsync();
        Task GetByCodigoAsync(int Codigo);
        Task<bool> Exists(int Codigo);

    }
}
