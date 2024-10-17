using Domain.Interfaces;
using Infra.Context;
using Infra.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
        EstoqueTemporarioRepository = new EstoqueTemporarioRepository(_context);
        EstoquePvbRepository = new EstoquePvbRepository(_context);
        EstoqueFabricanteRepository = new EstoqueFabricanteRepository(_context);
        MovimentacaoRepository = new MovimentacaoPvbRepository(_context);
    }

    public IEstoqueTemporarioRepository EstoqueTemporarioRepository { get; }
    public IEstoquePvbRepository EstoquePvbRepository { get; }
    public IEstoqueFabricanteRepository EstoqueFabricanteRepository { get; }
    public IMovimentacaoPvbRepository MovimentacaoRepository { get; }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync(); // Salva as alterações no contexto
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
