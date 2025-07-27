using Microsoft.AspNetCore.Http.HttpResults;

namespace AlmacenApi.DTO
{
    public class ProveedorAgregarDTO
    {

        public string Ruc { get; set; }

        public string Razon_social { get; set; }
        
        public string Direccion { get; set; }

        public string Telefono { get; set;}

        public string Email { get; set;}
    }
}
