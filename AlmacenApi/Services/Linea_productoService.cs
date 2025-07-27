using AlmacenApi.Context;
using AlmacenApi.Services.Interfaces;
using AlmacenApi.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using System.Data;

namespace AlmacenApi.Services
{
    public class Linea_productoService: ILinea_productoService
    {
        private readonly DBContext _dbContext;

        public Linea_productoService(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Linea_productoLisDTO>> BuscarLinea_producto(string? nombre)
        {
            var lista = new List<Linea_productoLisDTO>();
            using var conn = _dbContext.Database.GetDbConnection();
            await conn.OpenAsync();

            using var cmd = conn.CreateCommand();
            cmd.CommandText = "sp_buscarlineaProducto";
            cmd.CommandType = CommandType.StoredProcedure;


            var param = new SqlParameter("@Nombre", SqlDbType.VarChar, 100);
            param.Value = nombre ?? (object)DBNull.Value;
            cmd.Parameters.Add(param);

            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                lista.Add(new Linea_productoLisDTO
                {
                    IdLinea = reader.GetInt32(reader.GetOrdinal("idLinea")),
                    Nombre = reader.GetString(reader.GetOrdinal("nombre"))
                });
            }

            return lista;
        }

        public async Task<string> AgregarLinea_producto(Linea_productoAgregarDTO dto)
        {
            using var conn = _dbContext.Database.GetDbConnection();
            await conn.OpenAsync();

            using var cmd = conn.CreateCommand();
            cmd.CommandText = "sp_agregarLineaProducto";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@Nombre", dto.Nombre));

            try
            {
                await cmd.ExecuteNonQueryAsync();
                return "Linea de producto agregado correctamente";
            }
            catch (SqlException ex)
            {
                return ex.Message;
            }
        }

        public async Task<string> EditarLinea_producto(Linea_productoEditarDTO dto)
        {
            using var conn = _dbContext.Database.GetDbConnection();
            await conn.OpenAsync();

            using var cmd = conn.CreateCommand();
            cmd.CommandText = "sp_editarLineaProducto";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@idLinea", dto.IdLinea));
            cmd.Parameters.Add(new SqlParameter("@Nombre", string.IsNullOrWhiteSpace(dto.Nombre) ? DBNull.Value : dto.Nombre));

            try
            {
                await cmd.ExecuteNonQueryAsync();
                return "Linea de producto editado correctamente";
            }
            catch (SqlException ex)
            {
                return ex.Message;
            }
        }

        public async Task<string> EliminarLinea_producto(int idLinea)
        {
            using var conn = _dbContext.Database.GetDbConnection();
            await conn.OpenAsync();

            using var cmd = conn.CreateCommand();
            cmd.CommandText = "sp_eliminarLineaProducto";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@idLinea", idLinea));

            try
            {
                await cmd.ExecuteNonQueryAsync();
                return "Linea de producto eliminado correctamente";
            }
            catch (SqlException ex)
            {
                return ex.Message;
            }
        }
    }
}
