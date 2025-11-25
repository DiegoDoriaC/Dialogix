using Essalud.Domain;
using Essalud.Infraestructure.Database;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Essalud.Infraestructure.Repositories
{
    public class ResultadosRepository : IResultadosRepository
    {

        private readonly ISqlConnectionEssaludFactory _connectionFactory;

        public ResultadosRepository(ISqlConnectionEssaludFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<List<ResultadosClinicos>> ConsultarEstadoResultados(ResultadosClinicos resultado)
        {
            List<ResultadosClinicos> listadoResultados = new List<ResultadosClinicos>();
            ResultadosClinicos obj = new ResultadosClinicos();

            using (var connection = (SqlConnection)_connectionFactory.CreateConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "pr_consultar_estado_ultimos_resultados";
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@IdPaciente", resultado.CitaMedica.Paciente.IdPaciente);                    

                    await connection.OpenAsync();

                    using (var rd = await command.ExecuteReaderAsync())
                    {
                        while (await rd.ReadAsync())
                        {
                            obj = new ResultadosClinicos();
                            obj.IdResultadosClinicos = rd["IdResultadosClinicos"] != DBNull.Value ? Convert.ToInt32(rd["IdResultadosClinicos"]) : 0;
                            obj.TipoExamen = Convert.ToString(rd["TipoExamen"])!;
                            obj.Valor = Convert.ToString(rd["Valor"])!;
                            obj.Observaciones = Convert.ToString(rd["Observaciones"])!;
                            obj.Estado = Convert.ToString(rd["Estado"])!;
                            obj.FechaRegistro = rd["FechaRegistro"] != DBNull.Value ? Convert.ToDateTime(rd["FechaRegistro"]) : DateTime.MinValue;
                            obj.CitaMedica.Paciente.Nombre = Convert.ToString(rd["Nombre"])!;
                            listadoResultados.Add(obj);
                        }
                    }
                }
            }
            return listadoResultados;
        }

    }
}
