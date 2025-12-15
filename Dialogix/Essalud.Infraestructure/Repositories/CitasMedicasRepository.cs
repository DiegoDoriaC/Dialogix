using Essalud.Domain;
using Essalud.Domain.DTOs;
using Essalud.Infraestructure.Database;
using Essalud.Infraestructure.Repositories.Interfaces;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace Essalud.Infraestructure.Repositories
{
    public class CitasMedicasRepository : ICitasMedicasRepository
    {
        private readonly ISqlConnectionEssaludFactory _connectionFactory;

        public CitasMedicasRepository(ISqlConnectionEssaludFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }


        public async Task<bool> AgendarCitaMedica(CitaMedica cita)
        {
            bool result = false;

            using (var connection = (SqlConnection)_connectionFactory.CreateConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "pr_insertar_citamedica";
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@IdPaciente", cita.Paciente.IdPaciente);
                    command.Parameters.AddWithValue("@IdMedico", cita.Medico.IdMedico);
                    command.Parameters.AddWithValue("@FechaCita", cita.FechaCita);
                    command.Parameters.AddWithValue("@HoraCita", cita.HoraCita);
                    command.Parameters.AddWithValue("@Motivo", cita.Motivo);
                    command.Parameters.AddWithValue("@Estado", cita.Estado);

                    var outputIdParam = new SqlParameter("@xIdCitaMedica", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(outputIdParam);

                    await connection.OpenAsync();
                    result = await command.ExecuteNonQueryAsync() > 0;
                    cita.IdCitaMedica = (int)outputIdParam.Value;
                }
            }
            return result;
        }

        public async Task<bool> CancelarCitaMedica(CitaMedica cita)
        {
            bool result = false;

            using (var connection = (SqlConnection)_connectionFactory.CreateConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "pr_actualizar_estado_citamedica";
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@IdCitaMedica", cita.IdCitaMedica);
                    command.Parameters.AddWithValue("@Estado", "Cancelada");

                    await connection.OpenAsync();
                    result = await command.ExecuteNonQueryAsync() > 0;
                }
            }
            return result;
        }

        public async Task<List<CitaMedica>> HistorialCitasMedicas(CitaMedica cita)
        {
            List<CitaMedica> listadoCitas = new List<CitaMedica>();

            using (var connection = (SqlConnection)_connectionFactory.CreateConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "pr_listar_historial_citasmedicas";
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@IdPaciente", cita.Paciente.IdPaciente);
                    command.Parameters.AddWithValue("@Estado", cita.Estado);

                    await connection.OpenAsync();

                    using (var rd = await command.ExecuteReaderAsync())
                    {
                        while (await rd.ReadAsync())
                        {
                            listadoCitas.Add(new CitaMedica
                            {
                                IdCitaMedica = rd["Id"] != DBNull.Value ? Convert.ToInt32(rd["Id"]) : 0,
                                FechaCita = rd["FechaCita"] != DBNull.Value ? Convert.ToDateTime(rd["FechaCita"]) : DateTime.MinValue,
                                Estado = Convert.ToString(rd["Estado"])!,
                                Motivo = Convert.ToString(rd["Motivo"])!,
                                Paciente = new Paciente { Nombre = Convert.ToString(rd["NombrePaciente"])! },
                                Medico = new Medico { Nombre = Convert.ToString(rd["NombreMedico"])!, Especialidad = Convert.ToString(rd["Especialidad"])! }
                            });
                        }
                    }
                }
            }
            return listadoCitas;
        }

        public async Task<CitaMedica> InformacionCitaMedica(CitaMedica cita)
        {
            CitaMedica obj = new CitaMedica();

            using (var connection = (SqlConnection)_connectionFactory.CreateConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "pr_informacion_citamedica";
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@IdCitaMedica", cita.IdCitaMedica);

                    await connection.OpenAsync();

                    using (var rd = await command.ExecuteReaderAsync())
                    {
                        while (await rd.ReadAsync())
                        {
                            obj = new CitaMedica();
                            obj.IdCitaMedica = rd["Id"] != DBNull.Value ? Convert.ToInt32(rd["Id"]) : 0;
                            obj.Paciente.IdPaciente = rd["IdPaciente"] != DBNull.Value ? Convert.ToInt32(rd["IdPaciente"]) : 0;
                            obj.Medico.IdMedico = rd["IdMedico"] != DBNull.Value ? Convert.ToInt32(rd["IdMedico"]) : 0;
                            obj.FechaCita = rd["FechaCita"] != DBNull.Value ? Convert.ToDateTime(rd["FechaCita"]) : DateTime.MinValue;
                            obj.HoraCita = rd["HoraCita"] != DBNull.Value ? TimeOnly.FromTimeSpan((TimeSpan)rd["HoraCita"]) : TimeOnly.MinValue;
                            obj.Motivo = Convert.ToString(rd["Motivo"])!;
                            obj.Estado = Convert.ToString(rd["Estado"])!;
                        }
                    }
                }
            }
            return obj;
        }

        public async Task<List<CitasPorEspecialidadDTO>> ListarCantidadConsultasPorEspecialidad(DateTime FechaInicio, DateTime FechaFin)
        {
            List<CitasPorEspecialidadDTO> listado = new List<CitasPorEspecialidadDTO>();
            CitasPorEspecialidadDTO obj = new CitasPorEspecialidadDTO();

            using (var connection = (SqlConnection)_connectionFactory.CreateConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "pr_listar_atencion_por_especialidad";
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@FechaInicio", FechaInicio);
                    command.Parameters.AddWithValue("@FechaFin", FechaFin);

                    await connection.OpenAsync();

                    using (var rd = await command.ExecuteReaderAsync())
                    {
                        while (await rd.ReadAsync())
                        {
                            obj = new CitasPorEspecialidadDTO();
                            obj.Especilidad = rd["Especialidad"].ToString()!;
                            obj.Cantidad = rd["cantidad"] != DBNull.Value ? Convert.ToInt32(rd["cantidad"]) : 0;
                            listado.Add(obj);
                        }
                    }
                }
            }
            return listado;
        }

        public async Task<int> ObtenerTotalCitasAtendidas()
        {
            int total = 0;

            using (var connection = (SqlConnection)_connectionFactory.CreateConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "pr_total_citas_atendidas";
                    command.CommandType = CommandType.StoredProcedure;

                    await connection.OpenAsync();

                    var result = await command.ExecuteScalarAsync();
                    total = Convert.ToInt32(result);
                }
            }

            return total;
        }
        public async Task<List<CitasPorEspecialidadDTO>> ListarCitasPorEspecialidadTotales()
        {
            var lista = new List<CitasPorEspecialidadDTO>();

            using (var connection = (SqlConnection)_connectionFactory.CreateConnection())
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "pr_citas_por_especialidad";
                command.CommandType = CommandType.StoredProcedure;

                await connection.OpenAsync();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var item = new CitasPorEspecialidadDTO
                        {
                            Especilidad = reader["Especialidad"]?.ToString() ?? "",
                            Cantidad = reader["TotalCitas"] != DBNull.Value
                                ? Convert.ToInt32(reader["TotalCitas"])
                                : 0
                        };

                        lista.Add(item);
                    }
                }
            }

            return lista;
        }


    }
}
