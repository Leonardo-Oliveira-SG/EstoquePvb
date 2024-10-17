namespace Application.DTOs
{
    public class BuscaEstoqueDto
    {
        public int Codigo { get; set; }
        public string? TipoPvb { get; set; }
        public string? CodigoPvb { get; set; }
        public string? Fabricante { get; set; }
        public decimal Espessura { get; set; }
        public int TamanhoRolo { get; set; }
        public int Saldo { get; set; }
    }
}

