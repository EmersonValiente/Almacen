using AlmacenApi.Context;
using AlmacenApi.Services.Interfaces;
using AlmacenApi.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using System.Data;

namespace AlmacenApi.Services
{
    public class Pres_productoService: IPres_productoService
    {

        private readonly DBContext _dbContext;

        public Pres_productoService(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Pres_productoLisDTO>> BuscarPres_producto(string? nombre)
        {
            var lista = new List<Pres_productoLisDTO>();
            using var conn = _dbContext.Database.GetDbConnection();
            await conn.OpenAsync();

            using var cmd = conn.CreateCommand();
            cmd.CommandText = "sp_buscarpresProducto";
            cmd.CommandType = CommandType.StoredProcedure;

            var param = new SqlParameter("@Nombre", SqlDbType.VarChar, 100);
            param.Value = nombre ?? (object)DBNull.Value;
            cmd.Parameters.Add(param);

            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                lista.Add(new Pres_productoLisDTO
                {
                    IdPresentacion = reader.GetInt32(reader.GetOrdinal("idPresentacion")),
                    Nombre = reader.GetString(reader.GetOrdinal("nombre"))
                });
            }

            return lista;
        }

        public async Task<string> AgregarPres_producto(Pres_productoAgregarDTO dto)
        {
            using var conn = _dbContext.Database.GetDbConnection();
            await conn.OpenAsync();

            using var cmd = conn.CreateCommand();
            cmd.CommandText = "sp_agregarPresProducto";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@Nombre", dto.Nombre));

            try
            {
                await cmd.ExecuteNonQueryAsync();
                return "Presentación del producto agregado correctamente";
            }
            catch (SqlException ex)
            {
                return ex.Message;
            }
        }

        public async Task<string> EditarPres_producto(Pres_productoEditarDTO dto)
        {
            using var conn = _dbContext.Database.GetDbConnection();
            await conn.OpenAsync();

            using var cmd = conn.CreateCommand();
            cmd.CommandText = "sp_editarPresProducto";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@idPresentacion", dto.IdPresentacion));
            cmd.Parameters.Add(new SqlParameter("@Nombre", string.IsNullOrWhiteSpace(dto.Nombre) ? DBNull.Value : dto.Nombre));

            try
            {
                await cmd.ExecuteNonQueryAsync();
                return "Presentación del producto editado correctamente";
            }
            catch (SqlException ex)
            {
                return ex.Message;
            }
        }

        public async Task<string> EliminarPres_producto(int idPresentacion)
        {
            using var conn = _dbContext.Database.GetDbConnection();
            await conn.OpenAsync();

            using var cmd = conn.CreateCommand();
            cmd.CommandText = "sp_eliminarPresProducto";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@idPresentacion", idPresentacion));

            try
            {
                await cmd.ExecuteNonQueryAsync();
                return "Presentación del producto eliminado correctamente";
            }
            catch (SqlException ex)
            {
                return ex.Message;
            }
        }
    }
}
