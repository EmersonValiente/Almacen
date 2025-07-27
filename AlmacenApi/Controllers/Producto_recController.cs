using Microsoft.AspNetCore.Mvc;
using AlmacenApi.DTO;
using AlmacenApi.Services.Interfaces;
using AlmacenApi.Services;

namespace AlmacenApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Producto_recController : ControllerBase
    {
        private readonly IProducto_recService _productoRecService;

        public Producto_recController(IProducto_recService productoRecService)
        {
            _productoRecService = productoRecService;
        }

        [HttpGet("buscar")]
        public async Task<ActionResult<IEnumerable<Producto_recLisDTO>>> Buscar([FromQuery] string? busqueda)
        {
            return Ok(await _productoRecService.BuscarRecepcion(busqueda));
        }

        [HttpPost("agregar")]
        public async Task<ActionResult> Agregar([FromBody] Producto_recAgregarDTO dto)
        {
            var mensaje = await _productoRecService.AgregarRecepcion(dto);
            return Ok(new { mensaje });
        }

        [HttpPut("editar")]
        public async Task<ActionResult> Editar([FromBody] Producto_recEditarDTO dto)
        {
            var mensaje = await _productoRecService.EditarRecepcion(dto);
            return Ok(new { mensaje });
        }

        [HttpDelete("eliminar/{id}")]
        public async Task<ActionResult> Eliminar(int id)
        {
            var mensaje = await _productoRecService.EliminarRecepcion(id);
            return Ok(new { mensaje });
        }
    }
}
