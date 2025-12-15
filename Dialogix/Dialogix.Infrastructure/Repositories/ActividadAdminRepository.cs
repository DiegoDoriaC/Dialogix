using Dialogix.Domain;
using Dialogix.Infrastructure.Database;
using Essalud.Domain;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Dialogix.Infrastructure.Repositories
{
    public class ActividadAdminRepository : IActividadAdminRepository
    {
        private readonly ISqlConnectionDialogixFactory _connectionFactory;

        public ActividadAdminRepository(ISqlConnectionDialogixFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task RegistrarActividad(ActividadAdmin actividad)
        {
            using (var connection = (SqlConnection)_connectionFactory.CreateConnection())
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "pr_registrar_actividad_admin";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@IdUsuario", actividad.IdUsuario);
                command.Parameters.AddWithValue("@Modulo", actividad.Modulo);
                command.Parameters.AddWithValue("@Accion", actividad.Accion);
                command.Parameters.AddWithValue(
                    "@Detalle",
                    actividad.Detalle ?? (object)DBNull.Value
                );

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task<List<ActividadAdmin>> ListarUltimasActividades(int top)
        {
            List<ActividadAdmin> lista = new();

            using (var connection = (SqlConnection)_connectionFactory.CreateConnection())
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "pr_listar_ultimas_actividades_admin";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Top", top);

                await connection.OpenAsync();

                using (var dr = await command.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync())
                    {
                        lista.Add(new ActividadAdmin
                        {
                            IdActividad = Convert.ToInt32(dr["IdActividad"]),
                            IdUsuario = Convert.ToInt32(dr["IdUsuario"]),
                            Modulo = dr["Modulo"].ToString()!,
                            Accion = dr["Accion"].ToString()!,
                            Detalle = dr["Detalle"] == DBNull.Value ? null : dr["Detalle"].ToString(),
                            Fecha = Convert.ToDateTime(dr["Fecha"]),
                            AdminNombre = dr["AdminNombre"].ToString()
                        });
                    }
                }
            }

            return lista;
        }
    }
}
