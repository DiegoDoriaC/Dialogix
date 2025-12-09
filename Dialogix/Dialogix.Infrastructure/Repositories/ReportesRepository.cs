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
    public class ReportesRepository : IReportesRepository
    {
        private readonly ISqlConnectionDialogixFactory _connectionFactory;

        public ReportesRepository(ISqlConnectionDialogixFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<List<Feedback>> FiltrarFeedback(Feedback feedback)
        {
            List<Feedback> listado = new List<Feedback>();
            Feedback obj = new Feedback();

            using (var connection = (SqlConnection)_connectionFactory.CreateConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "pr_listar_feedback_rango_fechas";
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@FechaInicio", feedback.Fecha);
                    command.Parameters.AddWithValue("@FechaFin", feedback.FechaAux);
                    command.Parameters.AddWithValue("@Calificacion", feedback.Calificacion);
                    command.Parameters.AddWithValue("@Canal", feedback.Conversacion.Canal);

                    await connection.OpenAsync();

                    using (var rd = await command.ExecuteReaderAsync())
                    {
                        while (await rd.ReadAsync())
                        {
                            obj = new Feedback();
                            obj.Conversacion.IdConversacion = rd["IdConversacion"] != DBNull.Value ? Convert.ToInt32(rd["RegPorDia"]) : 0;
                            obj.Conversacion.Estado = Convert.ToString(rd["Estado"])!;
                            obj.Fecha = rd["Fecha"] != DBNull.Value ? Convert.ToDateTime(rd["Fecha"]) : DateTime.MinValue;
                            obj.Calificacion = rd["Calificacion"] != DBNull.Value ? Convert.ToInt32(rd["RegPorDia"]) : 0;
                            listado.Add(obj);
                        }
                    }
                }
            }
            return listado;
        }

        public async Task<int> FiltrarMetricaUso(DateTime FechaInicio, DateTime FechaFin)
        {
            int cantidadDeUsoTotal = 0;

            using (var connection = (SqlConnection)_connectionFactory.CreateConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "pr_filtrar_metrica_uso";
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@FechaInicio", FechaInicio);
                    command.Parameters.AddWithValue("@FechaFin", FechaFin);

                    await connection.OpenAsync();

                    using (var rd = await command.ExecuteReaderAsync())
                    {
                        while (await rd.ReadAsync())
                        {
                            cantidadDeUsoTotal = rd["consultasTotales"] != DBNull.Value ? Convert.ToInt32(rd["consultasTotales"]) : 0;
                        }
                    }
                }
            }
            return cantidadDeUsoTotal;
        }

        public async Task<List<MetricaUso>> FiltrarMetricaUsoPorDia(DateTime FechaInicio, DateTime FechaFin)
        {
            List<MetricaUso> listado = new List<MetricaUso>();
            MetricaUso obj = new MetricaUso();

            using (var connection = (SqlConnection)_connectionFactory.CreateConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "pr_filtrar_metrica_uso_por_dia";
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@FechaInicio", FechaInicio);
                    command.Parameters.AddWithValue("@FechaFin", FechaFin);

                    await connection.OpenAsync();

                    using (var rd = await command.ExecuteReaderAsync())
                    {
                        while (await rd.ReadAsync())
                        {
                            obj = new MetricaUso();
                            obj.Fecha = rd["Fecha"] != DBNull.Value ? Convert.ToDateTime(rd["Fecha"]) : DateTime.MinValue;
                            obj.TotalConversaciones = rd["RegPorDia"] != DBNull.Value ? Convert.ToInt32(rd["RegPorDia"]) : 0;
                            listado.Add(obj);
                        }
                    }
                }
            }
            return listado;
        }

        public async Task<List<MetricaUso>> FiltrarMetricaUsoPorMes(DateTime FechaInicio, DateTime FechaFin)
        {
            List<MetricaUso> listado = new List<MetricaUso>();
            MetricaUso obj = new MetricaUso();

            using (var connection = (SqlConnection)_connectionFactory.CreateConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "pr_filtrar_metrica_uso_por_mes";
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@FechaInicio", FechaInicio);
                    command.Parameters.AddWithValue("@FechaFin", FechaFin);

                    await connection.OpenAsync();

                    using (var rd = await command.ExecuteReaderAsync())
                    {
                        while (await rd.ReadAsync())
                        {
                            obj = new MetricaUso();
                            obj.NumeroMes = rd["NumeroMes"] != DBNull.Value ? Convert.ToInt32(rd["NumeroMes"]) : 0;
                            obj.TotalConversaciones = rd["RegPorMes"] != DBNull.Value ? Convert.ToInt32(rd["RegPorMes"]) : 0;
                            listado.Add(obj);
                        }
                    }
                }
            }
            return listado;
        }

        public async Task<bool> RegistrarMetrica()
        {
            bool respuesta = false;

            using (var connection = (SqlConnection)_connectionFactory.CreateConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "pr_registrar_metrica";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Fecha", DateTime.Now);

                    await connection.OpenAsync();
                    respuesta = await command.ExecuteNonQueryAsync() > 0;
                }
            }
            return respuesta;
        }

        public async Task<int> ObtenerMetricaHoy()
        {
            int total = 0;

            using (var connection = (SqlConnection)_connectionFactory.CreateConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "pr_metrica_hoy";
                    command.CommandType = CommandType.StoredProcedure;

                    await connection.OpenAsync();

                    using (var rd = await command.ExecuteReaderAsync())
                    {
                        if (await rd.ReadAsync())
                        {
                            total = rd[0] != DBNull.Value ? Convert.ToInt32(rd[0]) : 0;
                        }
                    }
                }
            }

            return total;
        }



    }
}
