using Microsoft.AspNetCore.Mvc;
using AlmacenApi.DTO;
using AlmacenApi.Services.Interfaces;

namespace AlmacenApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly IProductoService _productoService;

        public ProductoController(IProductoService productoService)
        {
            _productoService = productoService;
        }

        [HttpGet("buscar")]
        public async Task<ActionResult<IEnumerable<ProductoLisDTO>>> Buscar([FromQuery] string? nombre)
        {
            return Ok(await _productoService.BuscarProducto(nombre));
        }

        [HttpPost("agregar")]
        public async Task<ActionResult> Agregar([FromBody] ProductoAgregarDTO dto)
        {
            var mensaje = await _productoService.AgregarProducto(dto);
            return Ok(new { mensaje });
        }

        [HttpPut("editar")]
        public async Task<ActionResult> Editar([FromBody] ProductoEditarDTO dto)
        {
            var mensaje = await _productoService.EditarProducto(dto);
            return Ok(new { mensaje });
        }

        [HttpDelete("eliminar/{id}")]
        public async Task<ActionResult> Eliminar(int id)
        {
            var mensaje = await _productoService.EliminarProducto(id);
            return Ok(new { mensaje });
        }

    }
}
