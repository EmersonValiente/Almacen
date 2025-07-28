using AlmacenApi.Context;
using AlmacenApi.DTO;
using AlmacenApi.Services.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using AlmacenApi.Utils;

namespace AlmacenApi.Services
{
    public class UsuarioService: IUsuarioService
    {

        private readonly DBContext _dbContext;

        public UsuarioService(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<UsuarioLisDTO>> BuscarUsuarios(string? nombre)
        {
            var lista = new List<UsuarioLisDTO>();
            using var conn = _dbContext.Database.GetDbConnection();
            await conn.OpenAsync();

            using var cmd = conn.CreateCommand();
            cmd.CommandText = "sp_usuarioBuscar"; // nombre del procedimiento
            cmd.CommandType = CommandType.StoredProcedure;

            var param = new SqlParameter("@nombre", SqlDbType.VarChar, 50);
            param.Value = nombre ?? (object)DBNull.Value;
            cmd.Parameters.Add(param);

            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                lista.Add(new UsuarioLisDTO
                {
                    IdUsuario = reader.GetInt32(reader.GetOrdinal("idUsuario")),
                    Nombre = reader.GetString(reader.GetOrdinal("nombre"))
                });
            }

            return lista;
        }

        public async Task<string> AgregarUsuario(UsuarioAgregarDTO dto)
        {
            using var conn = _dbContext.Database.GetDbConnection();
            await conn.OpenAsync();

            using var cmd = conn.CreateCommand();
            cmd.CommandText = "sp_agregarUsuario";
            cmd.CommandType = CommandType.StoredProcedure;

            var hash = SeguridadService.HashSHA256(dto.Contrasenia);

            cmd.Parameters.Add(new SqlParameter("@nombre", dto.Nombre));
            cmd.Parameters.Add(new SqlParameter("@contrasenia", hash));

            try
            {
                await cmd.ExecuteNonQueryAsync();
                return "Usuario agregado correctamente";
            }
            catch (SqlException ex)
            {
                return ex.Message;
            }
        }

        public async Task<string> EditarUsuario(UsuarioEditarDTO dto)
        {
            using var conn = _dbContext.Database.GetDbConnection();
            await conn.OpenAsync();

            using var cmd = conn.CreateCommand();
            cmd.CommandText = "sp_editarUsuario";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@idUsuario", dto.IdUsuario));
            cmd.Parameters.Add(new SqlParameter("@nombre", string.IsNullOrWhiteSpace(dto.Nombre) ? DBNull.Value : dto.Nombre));

            var hash = string.IsNullOrWhiteSpace(dto.Contrasenia)
                ? (object)DBNull.Value
    :           SeguridadService.HashSHA256(dto.Contrasenia);

            cmd.Parameters.Add(new SqlParameter("@contrasenia", hash));

            try
            {
                await cmd.ExecuteNonQueryAsync();
                return "Usuario editado correctamente";
            }
            catch (SqlException ex)
            {
                return ex.Message;
            }
        }

        public async Task<string> EliminarUsuario(int idUsuario)
        {
            using var conn = _dbContext.Database.GetDbConnection();
            await conn.OpenAsync();

            using var cmd = conn.CreateCommand();
            cmd.CommandText = "sp_eliminarUsuario";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@idUsuario", idUsuario));

            try
            {
                await cmd.ExecuteNonQueryAsync();
                return "Usuario eliminado correctamente";
            }
            catch (SqlException ex)
            {
                return ex.Message;
            }
        }
    }
}
