namespace Domain.Entities
{
    public class EntradaRolo
    {
        public int Codigo { get; set; }
        public string? Fabricante { get; set; }
        public string? NotaFiscal { get; set; }
        public string? NumeroRolo { get; set; }
        public string? Destino { get; set; }

        public EntradaRolo() { }

        public EntradaRolo(int codigo, string? fabricante, string? notaFiscal, string? numeroRolo, string? destino)
        {
            Codigo = codigo;
            Fabricante = fabricante;
            NotaFiscal = notaFiscal;
            NumeroRolo = numeroRolo;
            Destino = destino;
        }
    }
}