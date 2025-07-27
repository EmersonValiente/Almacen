using Microsoft.AspNetCore.Mvc;
using AlmacenApi.DTO;
using AlmacenApi.Services.Interfaces;

namespace AlmacenApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Pres_productoController : ControllerBase
    {
        private readonly IPres_productoService _presService;

        public Pres_productoController(IPres_productoService presService)
        {
            _presService = presService;
        }

        [HttpGet("buscar")]
        public async Task<ActionResult<IEnumerable<Pres_productoLisDTO>>> Buscar([FromQuery] string? nombre)
        {
            return Ok(await _presService.BuscarPres_producto(nombre));
        }

        [HttpPost("agregar")]
        public async Task<ActionResult> Agregar([FromBody] Pres_productoAgregarDTO dto)
        {
            var mensaje = await _presService.AgregarPres_producto(dto);
            return Ok(new { mensaje });
        }

        [HttpPut("editar")]
        public async Task<ActionResult> Editar([FromBody] Pres_productoEditarDTO dto)
        {
            var mensaje = await _presService.EditarPres_producto(dto);
            return Ok(new { mensaje });
        }

        [HttpDelete("eliminar/{id}")]
        public async Task<ActionResult> Eliminar(int id)
        {
            var mensaje = await _presService.EliminarPres_producto(id);
            return Ok(new { mensaje });
        }
    }
}
