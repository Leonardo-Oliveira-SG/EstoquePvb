using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
    public class CoberturaDeEstoqueDto
    {
        public int Codigo { get; set; }
        public string? TipoPvb { get; set; }
        public string? CodigoPvb { get; set; }
        public int TamanhoRolo { get; set; }
        public decimal Espessura { get; set; }
        public int TotalUsado90Dias { get; set; }
        public decimal MediaConsumo { get; set; }
        public int EstoqueReferente { get; set; }
        public decimal CoberturaEstoque { get; set; }
        public int TotalUsado30Dias { get; set; }
    }
}

