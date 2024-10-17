namespace Application.DTOs
{
    public class PaginationRequest
    {
        public int PageNumber { get; set; } = 1; // Número da página solicitada.
        public int PageSize { get; set; } = 15;  // Tamanho da página (quantidade de itens por página).
    }
}

