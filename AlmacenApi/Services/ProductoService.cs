using AlmacenApi.Context;
using AlmacenApi.Services.Interfaces;
using AlmacenApi.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using System.Data;

namespace AlmacenApi.Services
{
    public class ProductoService: IProductoService
    {
        private readonly DBContext _dbContext;

        public ProductoService(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<ProductoLisDTO>> BuscarProducto(string? nombre)
        {
            var lista = new List<ProductoLisDTO>();
            using var conn = _dbContext.Database.GetDbConnection();
            await conn.OpenAsync();

            using var cmd = conn.CreateCommand();
            cmd.CommandText = "sp_buscarProducto";
            cmd.CommandType = CommandType.StoredProcedure;

            var param = new SqlParameter("@Nombre", SqlDbType.VarChar, 100);
            param.Value = nombre ?? (object)DBNull.Value;
            cmd.Parameters.Add(param);

            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                lista.Add(new ProductoLisDTO
                {
                    IdProducto = reader.GetInt32(reader.GetOrdinal("idProducto")),
                    Nombre = reader.GetString(reader.GetOrdinal("nombre")),
                    Descripcion = reader.GetString(reader.GetOrdinal("descripcion"))
                });
            }

            return lista;
        }

        public async Task<string> AgregarProducto(ProductoAgregarDTO dto)
        {
            using var conn = _dbContext.Database.GetDbConnection();
            await conn.OpenAsync();

            using var cmd = conn.CreateCommand();
            cmd.CommandText = "sp_agregarProducto";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@Nombre", dto.Nombre));
            cmd.Parameters.Add(new SqlParameter("@Descripcion", dto.Descripcion));

            try
            {
                await cmd.ExecuteNonQueryAsync();
                return "Producto agregado correctamente";
            }
            catch (SqlException ex)
            {
                return ex.Message;
            }
        }

        public async Task<string> EditarProducto(ProductoEditarDTO dto)
        {
            using var conn = _dbContext.Database.GetDbConnection();
            await conn.OpenAsync();

            using var cmd = conn.CreateCommand();
            cmd.CommandText = "sp_editarProducto";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@idProducto", dto.IdProducto));
            cmd.Parameters.Add(new SqlParameter("@Nombre", string.IsNullOrWhiteSpace(dto.Nombre) ? DBNull.Value : dto.Nombre));
            cmd.Parameters.Add(new SqlParameter("@Descripcion", string.IsNullOrWhiteSpace(dto.Descripcion) ? DBNull.Value : dto.Descripcion));

            try
            {
                await cmd.ExecuteNonQueryAsync();
                return "Producto editado correctamente";
            }
            catch (SqlException ex)
            {
                return ex.Message;
            }
        }

        public async Task<string> EliminarProducto(int idProducto)
        {
            using var conn = _dbContext.Database.GetDbConnection();
            await conn.OpenAsync();

            using var cmd = conn.CreateCommand();
            cmd.CommandText = "sp_eliminarProducto";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@IdProducto", idProducto));

            try
            {
                await cmd.ExecuteNonQueryAsync();
                return "Producto eliminado correctamente";
            }
            catch (SqlException ex)
            {
                return ex.Message;
            }
        }
    }
}
