using Application.DTOs; 
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("Api/[Controller]")]
    [ApiController]
    public class ControleEstoquePvbController : Controller
    {
        private readonly IEntradaRoloService _entradaRoloService;
        private readonly IBuscaService _buscaService;

        public ControleEstoquePvbController(IEntradaRoloService entradaRoloService, IBuscaService buscaService)
        {
            _entradaRoloService = entradaRoloService;
            _buscaService = buscaService;
        }

        [HttpPost("/entrada_estoque")]
        public async Task<ActionResult> Post([FromBody] EntradaRoloDto entradaRoloDto)
        {
            var resultado = await _entradaRoloService.IncrementarEstoque(entradaRoloDto);

            if (resultado != null)
                return Ok(resultado);

            return BadRequest("Erro ao adicionar item.");        

        }

        [HttpPost("/saida_estoque")]
        public async Task<IActionResult> BaixarEstoque([FromBody] SaidaRoloDto saidaRoloDto)
        {
            if (saidaRoloDto == null)
            {
                return BadRequest("Dados inválidos.");
            }

            try
            {
                var result = await _entradaRoloService.BaixarEstoque(saidaRoloDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao baixar o estoque: {ex.Message}");
            }
        }

        [HttpGet("/estoque_pvb")]
        public async Task<IActionResult> GetBuscaEstoque()
        {
            var estoquePvb = await _buscaService.GetBuscaEstoqueAsync();
            if (estoquePvb == null || !estoquePvb.Any())
            {
                return NotFound(new { message = "Não a itens no estoque para serem mostrados!" });
            }

            return Ok(estoquePvb);
        }

        [HttpGet("/estoque_fabricante")]
        public async Task<IActionResult> GetBuscaEstoqueFabricante()
        {
            var estoquePvbFabricante = await _buscaService.GetBuscaEstoqueFabricanteAsync();
            if (estoquePvbFabricante == null || !estoquePvbFabricante.Any())
            {
                return NotFound(new { message = "Não a itens no estoque para serem mostrados!" });
            }

            return Ok(estoquePvbFabricante);
        }

        [HttpGet("/estoque_temporario")]
        public async Task<IActionResult> GetBuscaEstoqueTemporario()
        {
            var estoquePvbTemporario = await _buscaService.GetBuscaEstoqueTemporarioAsync();
            if (estoquePvbTemporario == null || !estoquePvbTemporario.Any())
            {
                return NotFound(new { message = "Não a itens no estoque para serem mostrados!" });
            }

            return Ok(estoquePvbTemporario);
        }

        [HttpGet("/movimentacao_pvb")]
        public async Task<IActionResult> GetMovimentacoes([FromQuery] PaginationRequest paginationRequest)
        {
            var resultado = await _buscaService.GetBuscaMovimentacaoPvbAsync(paginationRequest);
            if (resultado == null)
            {
                return NotFound(new { message = "Não existe movimentações a serem mostrados!" });
            }

            return Ok(resultado);

        }

        [HttpGet("/cobertura_estoque")]
        public async Task<IActionResult> GetBuscaCoberturaDeEstoque()
        {
            var buscaCoberturaDeEstoque = await _buscaService.GetBuscaCoberturaDeEstoqueAsync();
            if (buscaCoberturaDeEstoque == null || !buscaCoberturaDeEstoque.Any())
            {
                return NotFound(new { message = "Não a itens no estoque para serem mostrados!" });
            }

            return Ok(buscaCoberturaDeEstoque);
        }

        [HttpGet("/cobertura_estoque_espesura")]
        public async Task<IActionResult> GetBuscaCoberturaDeEstoqueEspessura(decimal espessura)
        {
            var buscaCoberturaDeEstoque = await _buscaService.GetBuscaCoberturaDeEstoquePorEspessuraAsync(espessura);
            if (buscaCoberturaDeEstoque == null || !buscaCoberturaDeEstoque.Any())
            {
                return NotFound(new { message = "Não a itens no estoque para serem mostrados!" });
            }

            return Ok(buscaCoberturaDeEstoque);
        }

        [HttpGet("/cobertura_estoque_destino")]
        public async Task<IActionResult> GetBuscaCoberturaDeEstoqueDestino(string destino)
        {
            var buscaCoberturaDeEstoque = await _buscaService.GetBuscaCoberturaDeEstoquePorDestinoAsync(destino);
            if (buscaCoberturaDeEstoque == null || !buscaCoberturaDeEstoque.Any())
            {
                return NotFound(new { message = "Não a itens no estoque para serem mostrados!" });
            }

            return Ok(buscaCoberturaDeEstoque);
        }

        [HttpGet("/cobertura_estoque_espessura&destino")]
        public async Task<IActionResult> GetBuscaCoberturaDeEstoqueEspessuraAndDestino(decimal espessura, string destino)
        {
            var buscaCoberturaDeEstoque = await _buscaService.GetBuscaCoberturaDeEstoquePorEspessuraAndDestinoAsync(espessura, destino);
            if (buscaCoberturaDeEstoque == null || !buscaCoberturaDeEstoque.Any())
            {
                return NotFound(new { message = "Não a itens no estoque para serem mostrados!" });
            }

            return Ok(buscaCoberturaDeEstoque);
        }
    }
}


