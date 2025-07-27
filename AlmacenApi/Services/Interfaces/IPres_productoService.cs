using AlmacenApi.DTO;

namespace AlmacenApi.Services.Interfaces
{
    public interface IPres_productoService
    {
        Task<IEnumerable<Pres_productoLisDTO>> BuscarPres_producto(string? nombre);
        Task<string> AgregarPres_producto(Pres_productoAgregarDTO dto);
        Task<string> EditarPres_producto(Pres_productoEditarDTO dto);
        Task<string> EliminarPres_producto(int idPresentacion);
    }
}
