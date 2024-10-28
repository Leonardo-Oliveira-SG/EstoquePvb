using Domain.Entities;
using Domain.Interfaces;
using Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories
{
    public class PvbRepository : IPvbRepository
    {
        private readonly ApplicationDbContext _context;

        public PvbRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<Pvb> Pvb => _context.Set<Pvb>();

        public async Task AddAsync(Pvb entity)
        {
            await _context.AddAsync(entity);
        }

        public Task Update(Pvb entity)
        {
            _context.Pvb.Update(entity);
            return Task.CompletedTask;
        }

        public async Task<bool> DeleteById(int Codigo)
        {
            var pvb = await _context.Pvb.FindAsync(Codigo);
            if (pvb != null)
            {
                _context.Pvb.Remove(pvb);
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
           await _context.Pvb.AnyAsync(p => p.Codigo == Codigo);
        }

        public async Task<bool> Exists(int Codigo)
        {
            return await _context.Pvb.AnyAsync(p => p.Codigo == Codigo);
        }

    }
}

