using AlmacenApi.DTO;

namespace AlmacenApi.Services.Interfaces
{
    public interface IUsuarioService
    {

        Task<IEnumerable<UsuarioLisDTO>> BuscarUsuarios(string? nombre);
        Task<string> AgregarUsuario(UsuarioAgregarDTO dto);
        Task<string> EditarUsuario(UsuarioEditarDTO dto);
        Task<string> EliminarUsuario(int idUsuario);
    }
}
