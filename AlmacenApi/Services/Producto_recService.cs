using AlmacenApi.Context;
using AlmacenApi.Services.Interfaces;
using AlmacenApi.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using System.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AlmacenApi.Services
{
    public class Producto_recService: IProducto_recService
    {
        private readonly DBContext _dbContext;

        public Producto_recService(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Producto_recLisDTO>> BuscarRecepcion(string? busqueda)
        {
            var lista = new List<Producto_recLisDTO>();
            using var conn = _dbContext.Database.GetDbConnection();
            await conn.OpenAsync();

            using var cmd = conn.CreateCommand();
            cmd.CommandText = "sp_buscarRecepcion";
            cmd.CommandType = CommandType.StoredProcedure;

            var param = new SqlParameter("@busqueda", SqlDbType.VarChar, 200);
            param.Value = busqueda ?? (object)DBNull.Value;
            cmd.Parameters.Add(param);

            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                lista.Add(new Producto_recLisDTO
                {
                    IdRecepcion = reader.GetInt32(reader.GetOrdinal("idRecepcion")),
                    Nombre_Producto = reader.GetString(reader.GetOrdinal("Nombre_Producto")),
                    Presentacion = reader.GetString(reader.GetOrdinal("Presentacion")),
                    Cantidad = reader.GetInt32(reader.GetOrdinal("Cantidad")),
                    Precio = reader.GetDecimal(reader.GetOrdinal("Precio")),
                    Total = reader.GetDecimal(reader.GetOrdinal("Total")),
                    Categoria = reader.GetString(reader.GetOrdinal("Categoria")),
                    Ruc_Proveedor = reader.GetString(reader.GetOrdinal("Ruc_Proveedor")),
                    Nombre_Proveedor = reader.GetString(reader.GetOrdinal("Nombre_Proveedor"))
                });
            }

            return lista;
        }

        public async Task<string> AgregarRecepcion(Producto_recAgregarDTO dto)
        {
            using var conn = _dbContext.Database.GetDbConnection();
            await conn.OpenAsync();

            using var cmd = conn.CreateCommand();
            cmd.CommandText = "sp_agregarRecepcion";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@idProveedor", dto.IdProveedor));
            cmd.Parameters.Add(new SqlParameter("@idLinea", dto.IdLinea));
            cmd.Parameters.Add(new SqlParameter("@idPresentacion", dto.IdPresentacion));
            cmd.Parameters.Add(new SqlParameter("@idProducto", dto.IdProducto));
            cmd.Parameters.Add(new SqlParameter("@idUsuario", dto.IdUsuario));
            cmd.Parameters.Add(new SqlParameter("@Cantidad", dto.Cantidad));
            cmd.Parameters.Add(new SqlParameter("@Precio", dto.Precio));

            try
            {
                await cmd.ExecuteNonQueryAsync();
                return "Producto recepcionado agregado correctamente";
            }
            catch (SqlException ex)
            {
                return ex.Message;
            }
        }

        public async Task<string> EditarRecepcion(Producto_recEditarDTO dto)
        {
            using var conn = _dbContext.Database.GetDbConnection();
            await conn.OpenAsync();

            using var cmd = conn.CreateCommand();
            cmd.CommandText = "sp_editarRecepcion";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@idRecepcion", dto.IdRecepcion));
            cmd.Parameters.Add(new SqlParameter("@idProveedor", dto.IdProveedor));
            cmd.Parameters.Add(new SqlParameter("@idLinea", dto.IdLinea));
            cmd.Parameters.Add(new SqlParameter("@idPresentacion", dto.IdPresentacion));
            cmd.Parameters.Add(new SqlParameter("@idProducto", dto.IdProducto));
            cmd.Parameters.Add(new SqlParameter("@idUsuario", dto.IdUsuario));
            cmd.Parameters.Add(new SqlParameter("@Cantidad", dto.Cantidad));
            cmd.Parameters.Add(new SqlParameter("@Precio", dto.Precio));

            try
            {
                await cmd.ExecuteNonQueryAsync();
                return "Producto recepcionado editado correctamente";
            }
            catch (SqlException ex)
            {
                return ex.Message;
            }
        }

        public async Task<string> EliminarRecepcion(int idRecepcion)
        {
            using var conn = _dbContext.Database.GetDbConnection();
            await conn.OpenAsync();

            using var cmd = conn.CreateCommand();
            cmd.CommandText = "sp_eliminarRecepcion";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@idRecepcion", idRecepcion));

            try
            {
                await cmd.ExecuteNonQueryAsync();
                return "Producto recepcionado eliminado correctamente";
            }
            catch (SqlException ex)
            {
                return ex.Message;
            }
        }

    }
}
