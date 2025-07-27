namespace AlmacenApi.DTO
{
    public class Producto_recAgregarDTO
    {
        public int IdProveedor { get; set; }

        public int IdLinea { get; set; }

        public int IdPresentacion { get; set; }

        public int IdProducto { get; set; }

        public int IdUsuario { get; set; }

        public int Cantidad { get; set; }

        public decimal Precio { get; set; }
    }
}
