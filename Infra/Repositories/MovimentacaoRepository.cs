using Domain.Entities;
using Domain.Interfaces;
using Infra.Context;

namespace Infra.Repositories
{
    internal class MovimentacaoPvbRepository : IMovimentacaoPvbRepository
    {
        private readonly ApplicationDbContext _context;

        public IQueryable<MovimentacaoPvb> MovimentacaoPvb => _context.Set<MovimentacaoPvb>();

        public MovimentacaoPvbRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(MovimentacaoPvb entity)
        {
            await _context.AddAsync(entity);
        }

        public async Task Update(MovimentacaoPvb entity)
        {
            _context.MovimentacaoPvb.Update(entity);
        }

        public async Task<bool> DeleteById(int id)
        {
            var movimentacaoPvb = await _context.MovimentacaoPvb.FindAsync(id);
            if (movimentacaoPvb != null)
            {
                _context.MovimentacaoPvb.Remove(movimentacaoPvb);
                await _context.SaveChangesAsync();
                return true;
            }
            else { return false; }
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task GetByCodigoAsync(int Codigo)
        {
            return;
        }
    }
}
