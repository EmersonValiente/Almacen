using AlmacenApi.DTO;
using AlmacenApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AlmacenApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet("buscar")]
        public async Task<ActionResult<IEnumerable<UsuarioLisDTO>>> Buscar([FromQuery] string? nombre)
        {
            return Ok(await _usuarioService.BuscarUsuarios(nombre));
        }

        [HttpPost("agregar")]
        public async Task<ActionResult> Agregar([FromBody] UsuarioAgregarDTO dto)
        {
            var mensaje = await _usuarioService.AgregarUsuario(dto);
            return Ok(new { mensaje });
        }

        [HttpPut("editar")]
        public async Task<ActionResult> Editar([FromBody] UsuarioEditarDTO dto)
        {
            var mensaje = await _usuarioService.EditarUsuario(dto);
            return Ok(new { mensaje });
        }

        [HttpDelete("eliminar/{id}")]
        public async Task<ActionResult> Eliminar(int id)
        {
            var mensaje = await _usuarioService.EliminarUsuario(id);
            return Ok(new { mensaje });
        }
    }
}
