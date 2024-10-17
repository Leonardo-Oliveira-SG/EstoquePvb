using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class MovimentacaoPvb
    {
        [Key]
        public int Id { get; set; }

        public int Codigo { get; set; }
        public DateTime Data { get; set; }
        public string NumeroRolo { get; set; }
        public string Destino { get; set; }

        // Construtor para Entity Framework
        public MovimentacaoPvb() 
        {
            NumeroRolo = string.Empty;
            Destino = string.Empty;        
        }

        public MovimentacaoPvb(int codigo, DateTime data, string numeroRolo, string destino)
        {
            Codigo = codigo;
            Data = data;
            NumeroRolo = numeroRolo;
            Destino = destino;
        }
    }
}
