namespace Application.DTOs
{
    public class BuscaEstoqueTemporarioDto : BuscaEstoqueDto
    {
        public string? NotaFiscal { get; set; }
        public string? NumeroRolo { get; set; }
    }
}