using AlmacenApi.DTO;

namespace AlmacenApi.Services.Interfaces
{
    public interface ILinea_productoService
    {
        Task<IEnumerable<Linea_productoLisDTO>> BuscarLinea_producto(string? nombre);
        Task<string> AgregarLinea_producto(Linea_productoAgregarDTO dto);
        Task<string> EditarLinea_producto(Linea_productoEditarDTO dto);
        Task<string> EliminarLinea_producto(int idLinea);

    }
}
