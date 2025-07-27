using Microsoft.AspNetCore.Mvc;
using AlmacenApi.DTO;
using AlmacenApi.Services.Interfaces;

namespace AlmacenApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Linea_productoController : ControllerBase
    {
        private readonly ILinea_productoService _lineaService;

        public Linea_productoController(ILinea_productoService lineaService)
        {
            _lineaService = lineaService;
        }

        [HttpGet("buscar")]
        public async Task<ActionResult<IEnumerable<Linea_productoLisDTO>>> Buscar([FromQuery] string? nombre)
        {
            return Ok(await _lineaService.BuscarLinea_producto(nombre));
        }

        [HttpPost("agregar")]
        public async Task<ActionResult> Agregar([FromBody] Linea_productoAgregarDTO dto)
        {
            var mensaje = await _lineaService.AgregarLinea_producto(dto);
            return Ok(new { mensaje });
        }

        [HttpPut("editar")]
        public async Task<ActionResult> Editar([FromBody] Linea_productoEditarDTO dto)
        {
            var mensaje = await _lineaService.EditarLinea_producto(dto);
            return Ok(new { mensaje });
        }

        [HttpDelete("eliminar/{id}")]
        public async Task<ActionResult> Eliminar(int id)
        {
            var mensaje = await _lineaService.EliminarLinea_producto(id);
            return Ok(new { mensaje });
        }
    }
}
