using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class EstoquePvb
    {
        [Key]
        public int Codigo { get; set; }

        public int Saldo { get; set; }

        // Construtor para Entity Framework
        public EstoquePvb() { }

        public EstoquePvb(int codigo, int saldo)
        {
            Codigo = codigo;
            Saldo = saldo;
        }
    }
}
