using AlmacenApi.DTO;
using Microsoft.AspNetCore.Mvc;
using AlmacenApi.Services.Interfaces;

namespace AlmacenApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProveedorController : ControllerBase
    {

        private readonly IProveedorService _proveedorService;

        public ProveedorController(IProveedorService proveedorService)
        {
            _proveedorService = proveedorService;
        }

        [HttpGet("buscar")]
        public async Task<ActionResult<IEnumerable<ProveedorLisDTO>>> Buscar([FromQuery] string? busquedad)
        {
            return Ok(await _proveedorService.BuscarProveedor(busquedad));
        }

        [HttpPost("agregar")]
        public async Task<ActionResult> Agregar([FromBody] ProveedorAgregarDTO dto)
        {
            var mensaje = await _proveedorService.AgregarProveedor(dto);
            return Ok(new { mensaje });
        }

        [HttpPut("editar")]
        public async Task<ActionResult> Editar([FromBody] ProveedorEditarDTO dto)
        {
            var mensaje = await _proveedorService.EditarProveedor(dto);
            return Ok(new { mensaje });
        }

        [HttpDelete("eliminar/{id}")]
        public async Task<ActionResult> Eliminar(int id)
        {
            var mensaje = await _proveedorService.EliminarProveedor(id);
            return Ok(new { mensaje });
        }
    }
}
