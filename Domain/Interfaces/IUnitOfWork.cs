namespace Domain.Interfaces
{
   public interface IUnitOfWork : IDisposable
   {
     // Método para salvar mudanças no banco de dados
     Task SaveAsync();

     // Propriedades para acessar os repositórios
     IEstoqueTemporarioRepository EstoqueTemporarioRepository { get; }
     IEstoquePvbRepository EstoquePvbRepository { get; }
     IMovimentacaoPvbRepository MovimentacaoRepository { get; }
     IEstoqueFabricanteRepository EstoqueFabricanteRepository { get; }
     // Adicione outros repositórios conforme necessário
   }
}
