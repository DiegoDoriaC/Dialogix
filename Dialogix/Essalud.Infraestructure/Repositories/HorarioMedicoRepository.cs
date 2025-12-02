using Essalud.Domain;
using Essalud.Infraestructure.Database;
using Essalud.Infraestructure.Repositories.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Essalud.Infraestructure.Repositories
{
    public class HorarioMedicoRepository : IHorarioMedicoRepository
    {
        private readonly ISqlConnectionEssaludFactory _connectionFactory;

        public HorarioMedicoRepository(ISqlConnectionEssaludFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<HorarioMedico> ListarHorariosMedico(int idMedico, string fecha)
        {
            List<HorarioMedico> listadoCitas = new List<HorarioMedico>();
            HorarioMedico objeto = new HorarioMedico();

            using (var connection = (SqlConnection)_connectionFactory.CreateConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "pr_listar_horarios_medico";
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@IdMedico", idMedico);
                    command.Parameters.AddWithValue("@DiaSemana", fecha);

                    await connection.OpenAsync();

                    using (var rd = await command.ExecuteReaderAsync())
                    {
                        while (await rd.ReadAsync())
                        {
                            objeto = new HorarioMedico();
                            objeto.IdHorarioMedico = rd["Id"] != DBNull.Value ? Convert.ToInt32(rd["Id"]) : 0;
                            objeto.Medico.IdMedico = rd["IdMedico"] != DBNull.Value ? Convert.ToInt32(rd["IdMedico"]) : 0;
                            objeto.DiaSemana = Convert.ToString(rd["DiaSemana"])!;
                            objeto.HoraInicio = rd["HoraInicio"] != DBNull.Value ? TimeOnly.Parse(rd["HoraInicio"].ToString()!) : TimeOnly.MinValue;
                            objeto.HoraFin = rd["HoraFin"] != DBNull.Value ? TimeOnly.Parse(rd["HoraFin"].ToString()!) : TimeOnly.MaxValue;
                            listadoCitas.Add(objeto);
                        }
                    }
                }
            }
            return objeto;
        }

        public async Task<List<CitaMedica>> ListarHorariosOcupados(int idMedico, DateTime fecha)
        {
            List<CitaMedica> listadoCitas = new List<CitaMedica>();
            CitaMedica objeto = new CitaMedica();

            using (var connection = (SqlConnection)_connectionFactory.CreateConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "pr_listar_horarios_reservados";
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@IdMedico", idMedico);
                    command.Parameters.AddWithValue("@Fecha", fecha);

                    await connection.OpenAsync();

                    using (var rd = await command.ExecuteReaderAsync())
                    {
                        while (await rd.ReadAsync())
                        {
                            objeto = new CitaMedica();
                            objeto.HoraCita = rd["HoraCita"] != DBNull.Value ? TimeOnly.Parse(rd["HoraCita"].ToString()!) : TimeOnly.MinValue;
                            listadoCitas.Add(objeto);
                        }
                    }
                }
            }
            return listadoCitas;
        }

    }
}
