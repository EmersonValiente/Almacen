using AlmacenApi.DTO;

namespace AlmacenApi.Services.Interfaces
{
    public interface IProductoService
    {
        Task<IEnumerable<ProductoLisDTO>> BuscarProducto(string? nombre);
        Task<string> AgregarProducto(ProductoAgregarDTO dto);
        Task<string> EditarProducto(ProductoEditarDTO dto);
        Task<string> EliminarProducto(int idProducto);
    }
}
