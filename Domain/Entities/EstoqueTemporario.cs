using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class EstoqueTemporario
    {
        [Key]
        public int Id { get; set; }

        public int Codigo { get; set; }
        public string NotaFiscal { get; set; }
        public string Fabricante { get; set; }
        public int Saldo { get; set; }
        public string NumeroRolo { get; set; }            

        // Construtor para Entity Framework
        public EstoqueTemporario() 
        {
            NotaFiscal = string.Empty;
            NumeroRolo = string.Empty;
        }

        public EstoqueTemporario(int codigo, string notaFiscal, string fabricante, int saldo, string numeroRolo)
        {
            Codigo = codigo;
            NotaFiscal = notaFiscal;
            Fabricante = fabricante;
            Saldo = saldo;
            NumeroRolo = numeroRolo;
            
        }
    }
}
