using AlmacenApi.DTO;

namespace AlmacenApi.Services.Interfaces
{
    public interface IProveedorService
    {

        Task<IEnumerable<ProveedorLisDTO>> BuscarProveedor(string? busquedad);
        Task<string> AgregarProveedor(ProveedorAgregarDTO dto);
        Task<string> EditarProveedor(ProveedorEditarDTO dto);
        Task<string> EliminarProveedor(int idProveedor);
    }
}
