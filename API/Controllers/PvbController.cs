using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("Api/[Controller]")]
    [ApiController]
    public class PvbController : Controller
    {
        private readonly IPvbService _pvbService;

        public PvbController(IPvbService pvbService)
        {
            _pvbService = pvbService;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] AddPvbDto pvbDto)
        {
            var pvb = await _pvbService.Add(pvbDto);
            return Ok(pvb);
        }

        [HttpPut]
        public async Task<ActionResult> Put([FromBody] UpdatePvbDto pvbDto)
        {
            await _pvbService.Update(pvbDto);
            return NoContent();
        }

        [HttpDelete("{codigo}")]
        public async Task<ActionResult> Delete(int codigo)
        {
            var result = await _pvbService.Delete(codigo);

            if (result)
                return NoContent();
            else
                return NotFound();
        }

        [HttpGet("get-pagination")]
        public async Task<ActionResult> GetPagination([FromQuery] PaginationRequest paginationRequest)
        {
            var responseService = await _pvbService.GetFilter(paginationRequest);
            return Ok(responseService);
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var responseService = await _pvbService.Get();
            return Ok(responseService);
        }

        [HttpGet("{codigo}")]
        public async Task<ActionResult> GetById(int codigo)
        {
            var pvb = await _pvbService.GetById(codigo);
            return Ok(pvb);
        }
    }
}

