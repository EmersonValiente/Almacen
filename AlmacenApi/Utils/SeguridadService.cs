using System.Security.Cryptography;
using System.Text;

namespace AlmacenApi.Utils
{
    public static class SeguridadService
    {

        public static string HashSHA256(string textoPlano)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(textoPlano);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }
}
