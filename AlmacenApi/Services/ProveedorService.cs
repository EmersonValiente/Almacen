using AlmacenApi.Context;
using AlmacenApi.Services.Interfaces;
using AlmacenApi.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using System.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace AlmacenApi.Services
{
    public class ProveedorService: IProveedorService
    {
        private readonly DBContext _dbContext;

        public ProveedorService(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<ProveedorLisDTO>> BuscarProveedor(string? busquedad)
        {
            var lista = new List<ProveedorLisDTO>();
            using var conn = _dbContext.Database.GetDbConnection();
            await conn.OpenAsync();

            using var cmd = conn.CreateCommand();
            cmd.CommandText = "sp_buscarProveedor";
            cmd.CommandType = CommandType.StoredProcedure;

            var param = new SqlParameter("@busquedad", SqlDbType.VarChar, 200);
            param.Value = busquedad ?? (object)DBNull.Value;
            cmd.Parameters.Add(param);


            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                lista.Add(new ProveedorLisDTO
                {
                    IdProveedor = reader.GetInt32(reader.GetOrdinal("idProveedor")),
                    Ruc = reader.GetString(reader.GetOrdinal("ruc")),
                    Razon_social = reader.GetString(reader.GetOrdinal("razon_social")),
                    Direccion = reader.GetString(reader.GetOrdinal("direccion")),
                    Telefono = reader.GetString(reader.GetOrdinal("telefono")),
                    Email = reader.GetString(reader.GetOrdinal("email"))
                });
            }

            return lista;
        }

        public async Task<string> AgregarProveedor(ProveedorAgregarDTO dto)
        {
            using var conn = _dbContext.Database.GetDbConnection();
            await conn.OpenAsync();

            using var cmd = conn.CreateCommand();
            cmd.CommandText = "sp_agregarProveedor";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@Ruc", dto.Ruc));
            cmd.Parameters.Add(new SqlParameter("@Razon_social", dto.Razon_social));
            cmd.Parameters.Add(new SqlParameter("@Direccion", dto.Direccion));
            cmd.Parameters.Add(new SqlParameter("@Telefono", dto.Telefono));
            cmd.Parameters.Add(new SqlParameter("@Email", dto.Email));

            try
            {
                await cmd.ExecuteNonQueryAsync();
                return "Proveedor agregado correctamente";
            }
            catch (SqlException ex)
            {
                return ex.Message;
            }
        }

        public async Task<string> EditarProveedor(ProveedorEditarDTO dto)
        {
            using var conn = _dbContext.Database.GetDbConnection();
            await conn.OpenAsync();

            using var cmd = conn.CreateCommand();
            cmd.CommandText = "sp_editarProveedor";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@idProveedor", dto.IdProveedor));
            cmd.Parameters.Add(new SqlParameter("@Ruc", string.IsNullOrWhiteSpace(dto.Ruc) ? DBNull.Value : dto.Ruc));
            cmd.Parameters.Add(new SqlParameter("@Razon_social", string.IsNullOrWhiteSpace(dto.Razon_social) ? DBNull.Value : dto.Razon_social));
            cmd.Parameters.Add(new SqlParameter("@Direccion", string.IsNullOrWhiteSpace(dto.Direccion) ? DBNull.Value : dto.Direccion));
            cmd.Parameters.Add(new SqlParameter("@Telefono", string.IsNullOrWhiteSpace(dto.Telefono) ? DBNull.Value : dto.Telefono));
            cmd.Parameters.Add(new SqlParameter("@Email", string.IsNullOrWhiteSpace(dto.Email) ? DBNull.Value : dto.Email));

            try
            {
                await cmd.ExecuteNonQueryAsync();
                return "Proveedor editado correctamente";
            }
            catch (SqlException ex)
            {
                return ex.Message;
            }
        }

        public async Task<string> EliminarProveedor(int idProveedor)
        {
            using var conn = _dbContext.Database.GetDbConnection();
            await conn.OpenAsync();

            using var cmd = conn.CreateCommand();
            cmd.CommandText = "sp_eliminarProveedor";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@idProveedor", idProveedor));

            try
            {
                await cmd.ExecuteNonQueryAsync();
                return "Proveedor eliminado correctamente";
            }
            catch (SqlException ex)
            {
                return ex.Message;
            }
        }

    }
}
