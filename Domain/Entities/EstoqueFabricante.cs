using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class EstoqueFabricante
    {
        [Key]
        public Guid Id { get; set; }

        public int Codigo { get; set; }
        public string Fabricante { get; set; }
        public int Saldo { get; set; }

        // Construtor para Entity Framework
        public EstoqueFabricante() { }

        public EstoqueFabricante(Guid id, int codigo, string fabricante, int saldo)
        {
            Id = id;
            Codigo = codigo;
            Fabricante = fabricante;
            Saldo = saldo;
        }
    }
}

