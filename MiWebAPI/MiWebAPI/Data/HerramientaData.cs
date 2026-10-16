using MiWebAPI.Controllers;
using MiWebAPI.Models;
using System.Data;
using System.Data.SqlClient;

namespace MiWebAPI.Data
{
    public class HerramientaData
    {
        private readonly string conexion;
        public HerramientaData(IConfiguration configuration)
        {
            conexion = configuration.GetConnectionString("CadenaSQL")!;
        }

        public async Task<List<Herramienta>> ConsultarTodos()
        {
            List<Herramienta> lista = new List<Herramienta>();

            using (var con = new SqlConnection(conexion))
            {
                await con.OpenAsync();
                SqlCommand cmd = new SqlCommand("dbo.Herramienta_ConsultarTodos", con);
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

        public async Task<Herramienta> SeleccionarPorId(long IdHerramienta)
        {
            Herramienta objeto = new Herramienta();

            using (var con = new SqlConnection(conexion))
            {
                await con.OpenAsync();
                SqlCommand cmd = new SqlCommand("dbo.Herramienta_ConsultarPorId", con);
                cmd.Parameters.AddWithValue("@idHerramienta", IdHerramienta);
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



        public async Task<bool> Registrar(Herramienta objeto)
        {
            bool respuesta = true;

            using (var con = new SqlConnection(conexion))
            {
                SqlCommand cmd = new SqlCommand("dbo.Herramienta_Insertar", con);
                cmd.Parameters.AddWithValue("@Codigo", objeto.Codigo ?? "");
                cmd.Parameters.AddWithValue("@Nombre", objeto.Nombre ?? "");
                cmd.Parameters.AddWithValue("@Marca", objeto.Marca ?? "");
                cmd.Parameters.AddWithValue("@Descripcion", objeto.Descripcion ?? "");
                cmd.Parameters.AddWithValue("@Costo", objeto.Costo);

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
        private static Herramienta Mapear(System.Data.Common.DbDataReader reader)
        {
            return new Herramienta
            {
                // Campos comunes
                IdHerramienta = Convert.ToInt32(reader["IdHerramienta"]),
                Activo = Convert.ToBoolean(reader["Activo"]),
                // Campos específicos
                Codigo = reader["Codigo"].ToString(),
                Nombre = reader["Nombre"].ToString(),
                Marca = reader["Marca"].ToString(),
                // Descripcion permite nulos en la tabla, así que validamos
                Descripcion = reader["Descripcion"] is DBNull ? null : reader["Descripcion"].ToString(),
                Costo = Convert.ToDecimal(reader["Costo"])
            };
        }

        public async Task<bool> Actualizar(Herramienta objeto)
        {
            bool respuesta = true;

            using (var con = new SqlConnection(conexion))
            {
                SqlCommand cmd = new SqlCommand("dbo.Herramienta_Actualizar", con);
                cmd.Parameters.AddWithValue("@IdHerramienta", objeto.IdHerramienta);
                cmd.Parameters.AddWithValue("@Codigo", objeto.Codigo ?? "");
                cmd.Parameters.AddWithValue("@Nombre", objeto.Nombre ?? "");
                cmd.Parameters.AddWithValue("@Marca", objeto.Marca ?? "");
                cmd.Parameters.AddWithValue("@Descripcion", objeto.Descripcion ?? "");
                cmd.Parameters.AddWithValue("@Costo", objeto.Costo);
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

        public async Task<bool> DarDeBaja(long IdHerramienta)
        {
            bool respuesta = true;

            using (var con = new SqlConnection(conexion))
            {
                // Se llama al procedimiento específico para la baja lógica de la herramienta
                SqlCommand cmd = new SqlCommand("dbo.Herramienta_Eliminar", con);

                // El parámetro debe coincidir exactamente con el nombre en tu procedimiento almacenado
                cmd.Parameters.AddWithValue("@IdHerramienta", IdHerramienta);

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
