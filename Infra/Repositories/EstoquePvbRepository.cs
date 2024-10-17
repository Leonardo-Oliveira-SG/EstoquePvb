using Domain.Entities;
using Domain.Interfaces;
using Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories
{
    internal class EstoquePvbRepository : IEstoquePvbRepository
    {
        private readonly ApplicationDbContext _context;

        public EstoquePvbRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        

        public async Task AddAsync(EstoquePvb entity)
        {
            await _context.AddAsync(entity);
        }

        public async Task Update(EstoquePvb entity)
        {
            _context.EstoquePvb.Update(entity);
        }

        public async Task<bool> DeleteById(Guid id)
        {
            var estoquePvb = await _context.EstoquePvb.FindAsync(id);
            if (estoquePvb != null)
            {
                _context.EstoquePvb.Remove(estoquePvb);
                await _context.SaveChangesAsync();
                return true;
            }
            else { return false; }
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<EstoquePvb> GetByCodigoAsync(int Codigo)
        {
            // Implementação da busca no banco de dados
            var estoquePvb = await _context.EstoquePvb.FirstOrDefaultAsync(e => e.Codigo == Codigo);

            return estoquePvb;
        }

        public async Task<bool> Exists(int Codigo)
        {
            return await _context.EstoquePvb.AnyAsync(e => e.Codigo == Codigo);
        }

        public async Task RemoveAsync(EstoquePvb estoquePvb)
        {
            _context.Set<EstoquePvb>().Remove(estoquePvb);
            await _context.SaveChangesAsync();
        }

        public IQueryable<EstoquePvb> EstoquePvb => _context.Set<EstoquePvb>();

    }
}
