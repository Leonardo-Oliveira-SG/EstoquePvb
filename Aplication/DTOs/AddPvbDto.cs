namespace Application.DTOs
{
    public class AddPvbDto
    {
        public required string TipoPvb { get; set; }
        public required string CodigoPvb { get; set; }
        public required string Fabricante { get; set; }
        public required decimal Espessura { get; set; }
        public required int TamanhoRolo { get; set; }
    }
}
