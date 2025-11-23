using Dialogix.Domain;
using Dialogix.Infrastructure.Database;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dialogix.Infrastructure.Repositories
{
    public class PreguntaFrencuente
    {
        private readonly ISqlConnectionFactory _connectionFactory;

        public PreguntaFrencuente(ISqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<List<PreguntasFrecuentes>> ListarPreguntasFrecuentes()
        {
            List<PreguntasFrecuentes> listadoPreFrec = new List<PreguntasFrecuentes>();
            PreguntasFrecuentes obj = new PreguntasFrecuentes();

            using (var connection = (SqlConnection)_connectionFactory.CreateConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "pr_listar_preguntas_frecuentes";
                    command.CommandType = CommandType.StoredProcedure;

                    await connection.OpenAsync();

                    using (var rd = await command.ExecuteReaderAsync())
                    {
                        while (await rd.ReadAsync())
                        {
                            obj = new PreguntasFrecuentes();
                            obj.IdPreguntaFrecuente = rd["IdPreguntaFrecuente"] != DBNull.Value ? Convert.ToInt32(rd["IdPreguntaFrecuente"]) : 0;
                            obj.Descripcion = Convert.ToString(rd["Descripcion"])!;
                            obj.Orden = rd["Orden"] != DBNull.Value ? Convert.ToInt32(rd["Orden"]) : 0;
                            listadoPreFrec.Add(obj);
                        }
                    }
                }
            }
            return listadoPreFrec;
        }


        public async Task<bool> RegistrarPreguntaFrecuente (PreguntasFrecuentes pre)
        {
            bool result = false;

            using (var connection = (SqlConnection)_connectionFactory.CreateConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "pr_registrar_pregunta_frecuente";
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Descripcion", pre.Descripcion);
                    command.Parameters.AddWithValue("@Orden", pre.Orden);

                    await connection.OpenAsync();
                    result = await command.ExecuteNonQueryAsync() > 0;
                }
            }
            return result;
        }


        public async Task<bool> ModificarPreguntaFrecuente (PreguntasFrecuentes pre)
        {
            bool result = false;

            using (var connection = (SqlConnection)_connectionFactory.CreateConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "pr_modificar_pregunta_frecuente";
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@IdPreguntaFrecuente", pre.IdPreguntaFrecuente);
                    command.Parameters.AddWithValue("@Descripcion", pre.Descripcion);
                    command.Parameters.AddWithValue("@Orden", pre.Orden);

                    await connection.OpenAsync();
                    result = await command.ExecuteNonQueryAsync() > 0;
                }
            }
            return result;
        }


        public async Task<bool> EliminarPreguntaFrecuente (PreguntasFrecuentes pre)
        {
            bool result = false;

            using (var connection = (SqlConnection)_connectionFactory.CreateConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "pr_eliminar_preguntas_frecuentes";
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@IdPreguntaFrecuente", pre.IdPreguntaFrecuente);

                    await connection.OpenAsync();
                    result = await command.ExecuteNonQueryAsync() > 0;
                }
            }
            return result;
        }


    }
}
