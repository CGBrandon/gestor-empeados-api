using MiWebAPI.Controllers;
using MiWebAPI.Models;
using System.Data;
using System.Data.SqlClient;

namespace MiWebAPI.Data
{
    public class ProveedorData
    {
        private readonly string conexion;
        public ProveedorData(IConfiguration configuration)
        {
            conexion = configuration.GetConnectionString("CadenaSQL")!;
        }
        
        public async Task<List<Proveedor>> Seleccionar()
        {
            List<Proveedor> lista = new List<Proveedor>();

            using (var con = new SqlConnection(conexion))
            {
                await con.OpenAsync();
                SqlCommand cmd = new SqlCommand("dbo.PROVEEDOR_SELECCIONAR", con);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        lista.Add(Mapear(reader));
                    }
                }
            }
            return lista;
        }

        public async Task<Proveedor> SeleccionarPorId(long id)
        {
            Proveedor objeto = new Proveedor();

            using (var con = new SqlConnection(conexion))
            {
                await con.OpenAsync();
                SqlCommand cmd = new SqlCommand("dbo.PROVEEDOR_SELECCIONAR_POR_ID", con);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        objeto = Mapear(reader);
                    }
                }
            }
            return objeto;
        }



        public async Task<bool> Crear(Proveedor objeto)
        {
            bool respuesta = true;

            using (var con = new SqlConnection(conexion))
            {
                SqlCommand cmd = new SqlCommand("dbo.PROVEEDOR_CREAR", con);
                cmd.Parameters.AddWithValue("@Nombre", objeto.Nombre ?? "");
                cmd.Parameters.AddWithValue("@ApellidoPaterno", objeto.ApellidoPaterno);
                cmd.Parameters.AddWithValue("@ApellidoMaterno", objeto.ApellidoMaterno);
                cmd.Parameters.AddWithValue("@Rfc", objeto.Rfc);
                cmd.Parameters.AddWithValue("@Correo", objeto.Correo);
                cmd.Parameters.AddWithValue("@Credito", objeto.Credito);
                cmd.Parameters.AddWithValue("@NombreBanco", objeto.NombreBanco);

                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    await con.OpenAsync();
                    respuesta = await cmd.ExecuteNonQueryAsync() > 0 ? true : false;
                }
                catch
                {
                    respuesta = false;
                }
            }
            return respuesta;
        }
        private static Proveedor Mapear(System.Data.Common.DbDataReader reader)
        {
            return new Proveedor
            {
                //Id = Convert.ToInt64(reader["Id"]),
                Nombre = reader["Nombre"].ToString(),
                ApellidoPaterno = reader["ApellidoPaterno"].ToString(),
                ApellidoMaterno = reader["ApellidoMaterno"].ToString(),
                Rfc = reader["Rfc"].ToString(),
                Correo = reader["Correo"].ToString(),
                Credito = reader["Credito"] is DBNull ? 0 : Convert.ToDecimal(reader["Credito"]),
                NombreBanco = reader["NombreBanco"].ToString()
            };
        }

        public async Task<bool> Actualizar(Proveedor objeto)
        {
            bool respuesta = true;

            using (var con = new SqlConnection(conexion))
            {
                SqlCommand cmd = new SqlCommand("dbo.PROVEEDOR_ACTUALIZAR", con);
                cmd.Parameters.AddWithValue("@id", objeto.Id);
                cmd.Parameters.AddWithValue("@Nombre", objeto.Nombre ?? "");
                cmd.Parameters.AddWithValue("@ApellidoPaterno", objeto.ApellidoPaterno ?? "");
                cmd.Parameters.AddWithValue("@ApellidoMaterno", objeto.ApellidoMaterno ?? "");
                cmd.Parameters.AddWithValue("@Rfc", objeto.Rfc ?? "");
                cmd.Parameters.AddWithValue("@Correo", objeto.Correo ?? "");
                cmd.Parameters.AddWithValue("@Credito", objeto.Credito);
                cmd.Parameters.AddWithValue("@NombreBanco", objeto.NombreBanco);
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    await con.OpenAsync();
                    respuesta = await cmd.ExecuteNonQueryAsync() > 0 ? true : false;
                }
                catch
                {
                    respuesta = false;
                }
            }
            return respuesta;
        }

        public async Task<bool> Eliminar(long id)
        {
            bool respuesta = true;

            using (var con = new SqlConnection(conexion))
            {
                SqlCommand cmd = new SqlCommand("dbo.PROVEEDOR_ELIMINAR", con);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    await con.OpenAsync();
                    respuesta = await cmd.ExecuteNonQueryAsync() > 0 ? true : false;
                }
                catch
                {
                    respuesta = false;
                }
            }
            return respuesta;
        }

        
    }
}
