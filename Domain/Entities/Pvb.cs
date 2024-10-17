using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Pvb
    {
        [Key] // Identifica que este campo é a chave primária
        public int Codigo { get; set; } // O campo autoincrementado

        public string TipoPvb { get; set; }
        public string CodigoPvb { get; set; }
        public string Fabricante { get; set; }
        public decimal Espessura { get; set; }
        public int TamanhoRolo { get; set; }

        public Pvb()
        {
            TipoPvb = string.Empty;
            CodigoPvb = string.Empty;
            Fabricante = string.Empty;
        }

        public Pvb(string codigoPvb, string tipoPvb, decimal espessura, int tamanhoRolo, String fabricante)
        {
            
            CodigoPvb = codigoPvb;
            Fabricante = fabricante;
            CodigoPvb = codigoPvb;
            TipoPvb = tipoPvb;
            Espessura = espessura;
            TamanhoRolo = tamanhoRolo;
        }
    }
}
