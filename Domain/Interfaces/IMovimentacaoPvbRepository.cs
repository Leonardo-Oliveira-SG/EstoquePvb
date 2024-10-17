using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IMovimentacaoPvbRepository
    {
        
        IQueryable<MovimentacaoPvb> MovimentacaoPvb { get; }

        Task AddAsync(MovimentacaoPvb movimentacaoPvb);
        Task Update(MovimentacaoPvb movimentacaoPvb);
        Task<bool> DeleteById(int Id);
        Task SaveChangesAsync();
    }
}
