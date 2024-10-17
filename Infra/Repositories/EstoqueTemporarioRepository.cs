using Domain.Entities;
using Domain.Interfaces;
using Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories
{
    internal class EstoqueTemporarioRepository : IEstoqueTemporarioRepository
    {
        private readonly ApplicationDbContext _context;

        public EstoqueTemporarioRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<EstoqueTemporario> EstoqueTemporario => _context.Set<EstoqueTemporario>();

        public async Task AddAsync(EstoqueTemporario entity)
        {
            await _context.AddAsync(entity);
        }

        public async Task Update(EstoqueTemporario entity)
        {
            _context.EstoqueTemporario.Update(entity);
        }

        public async Task<bool> DeleteById(Guid id)
        {
            var estoqueTemporario = await _context.EstoqueTemporario.FindAsync(id);
            if (estoqueTemporario != null)
            {
                _context.EstoqueTemporario.Remove(estoqueTemporario);
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

        public async Task<bool> Exists(string NumeroRolo)
        {
            return await _context.EstoqueTemporario.AnyAsync(e => e.NumeroRolo == NumeroRolo);
        }

        public async Task RemoveAsync(EstoqueTemporario estoqueTemporario)
        {
            _context.Set<EstoqueTemporario>().Remove(estoqueTemporario);
            await _context.SaveChangesAsync();
        }

        public async Task<EstoqueTemporario> GetByNumeroRoloAsync(string numeroRolo)
        {
            return await _context.Set<EstoqueTemporario>().FirstOrDefaultAsync(e => e.NumeroRolo == numeroRolo);
        }
    }
}