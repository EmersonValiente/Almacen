using AlmacenApi.DTO;

namespace AlmacenApi.Services.Interfaces
{
    public interface IProducto_recService
    {
        Task<IEnumerable<Producto_recLisDTO>> BuscarRecepcion(string? busqueda);
        Task<string> AgregarRecepcion(Producto_recAgregarDTO dto);
        Task<string> EditarRecepcion(Producto_recEditarDTO dto);
        Task<string> EliminarRecepcion(int idProducto);
    }
}
