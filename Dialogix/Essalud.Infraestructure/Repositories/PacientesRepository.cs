using Essalud.Domain;
using Essalud.Infraestructure.Database;
using Essalud.Infraestructure.Repositories.Interfaces;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Essalud.Infraestructure.Repositories
{
    public class PacientesRepository : IPacientesRepository
    {
        private readonly ISqlConnectionEssaludFactory _connectionFactory;

        public PacientesRepository(ISqlConnectionEssaludFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<Paciente> BuscarUsuarioPorDni(Paciente paciente)
        {
            Paciente obj = new Paciente();

            using (var connection = (SqlConnection)_connectionFactory.CreateConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "pr_validar_existencia_paciente";
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@DniPaciente", paciente.Dni);

                    await connection.OpenAsync();

                    using (var rd = await command.ExecuteReaderAsync())
                    {
                        while (await rd.ReadAsync())
                        {
                            obj.IdPaciente = rd["Id"] != DBNull.Value ? Convert.ToInt32(rd["Id"]) : 0;
                            obj.Dni = Convert.ToString(rd["DNI"])!;
                            obj.Nombre = Convert.ToString(rd["Nombre"])!;
                            obj.Sexo = Convert.ToString(rd["Sexo"])!;
                            obj.FechaNacimiento = rd["FechaNacimiento"] != DBNull.Value ? Convert.ToDateTime(rd["FechaNacimiento"]) : DateTime.MinValue;
                            obj.Telefono = Convert.ToString(rd["Telefono"])!;
                            obj.Correo = Convert.ToString(rd["Correo"])!;
                            obj.Direccion = Convert.ToString(rd["Direccion"])!;
                        }
                    }
                }
            }
            return obj;
        }

        public async Task<string> ObtenerUltimoDigitoDni(int idPaciente)
        {
            string result = "";

            using (var connection = (SqlConnection)_connectionFactory.CreateConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "pr_obtener_ult_dig";
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@ID_PAC", idPaciente);

                    await connection.OpenAsync();
                    result = command.ExecuteScalar().ToString()!;
                }
            }
            return result;
        }
        public async Task<bool> ValidarFechaNacimiento(int idPaciente, DateTime fecha)
        {
            using (var connection = (SqlConnection)_connectionFactory.CreateConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "pr_validar_fecha_nacimiento";
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@IdPaciente", idPaciente);
                    command.Parameters.AddWithValue("@FechaNacimiento", fecha);

                    await connection.OpenAsync();
                    int result = Convert.ToInt32(await command.ExecuteScalarAsync());

                    return result == 1;
                }
            }
        }

    }
}
