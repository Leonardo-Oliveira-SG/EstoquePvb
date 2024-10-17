namespace Application.DTOs
{
    public class BuscaMovimentacaoPvbDto : PvbDto
    {
        public string? NotaFiscal { get; set; }
        public string? NumeroRolo { get; set; }
        public string? Destino { get; set; }
        public DateTime Data { get; set; }
    }
}
