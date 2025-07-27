namespace AlmacenApi.DTO
{
    public class Producto_recLisDTO
    {

        public int IdRecepcion { get; set; }

        public string Nombre_Producto { get; set; }

        public string Presentacion { get; set;}

        public int Cantidad { get; set; }

        public decimal Precio { get; set; }

        public decimal Total { get; set; }

        public string Categoria { get; set; }

        public string Ruc_Proveedor { get; set; }

        public string Nombre_Proveedor { get; set; }

    }
}
