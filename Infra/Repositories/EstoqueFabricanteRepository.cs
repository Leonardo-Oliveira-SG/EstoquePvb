using Domain.Entities;
using Domain.Interfaces;
using Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories
{
    public class EstoqueFabricanteRepository : IEstoqueFabricanteRepository
    {
        private readonly ApplicationDbContext _context;

        public EstoqueFabricanteRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(EstoqueFabricante entity)
        {
            await _context.AddAsync(entity);
        }

        public async Task Update(EstoqueFabricante estoqueFabricante)
        {
            _context.EstoqueFabricante.Update(estoqueFabricante);
        }

        public async Task<bool> DeleteByFabricanteAndCodigo(string Fabricante, int Codigo)
        {
            var estoqueFabricante = await _context.EstoqueFabricante
                .FirstOrDefaultAsync(et => et.Fabricante == Fabricante && et.Codigo == Codigo);
            if (estoqueFabricante != null)
            {
                _context.EstoqueFabricante.Remove(estoqueFabricante);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<EstoqueFabricante?> GetByFabricanteAndCodigo(string Fabricante, int Codigo)
        {
            try
            {
                var estoqueFabricante = await _context.EstoqueFabricante
                    .FirstOrDefaultAsync(e => e.Fabricante == Fabricante && e.Codigo == Codigo);

                return estoqueFabricante;
            }
            catch (InvalidCastException)
            {
                // Log o erro ou trate-o conforme necessário
                throw new Exception("Erro ao buscar o estoque fabricante");
            }


        }

        public async Task RemoveAsync(EstoqueFabricante estoqueFabricante)
        {
            _context.Set<EstoqueFabricante>().Remove(estoqueFabricante);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(string fabricante, int codigo)
        {
            var estoqueFabricante = await _context.Set<EstoqueFabricante>().FirstOrDefaultAsync(e => e.Codigo == codigo);
            if (estoqueFabricante != null)
            {
                _context.Set<EstoqueFabricante>().Remove(estoqueFabricante);
                await _context.SaveChangesAsync();
            }
        }

        public IQueryable<EstoqueFabricante> EstoqueFabricante => _context.Set<EstoqueFabricante>();
    }
}

