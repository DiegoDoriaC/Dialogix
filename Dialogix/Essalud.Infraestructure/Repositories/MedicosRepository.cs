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
    public class MedicosRepository : IMedicosRepository
    {
        private readonly ISqlConnectionEssaludFactory _connectionFactory;

        public MedicosRepository(ISqlConnectionEssaludFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<List<Medico>> ListarMedicosSegunEspecialidad(string especialidad)
        {
            List<Medico> listadoMedicos = new List<Medico>();
            Medico medico = new Medico();

            using (var connection = (SqlConnection)_connectionFactory.CreateConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "pr_listar_doctores_segun_especialidad";
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Especialidad", especialidad);

                    await connection.OpenAsync();

                    using (var rd = await command.ExecuteReaderAsync())
                    {
                        while (await rd.ReadAsync())
                        {
                            medico = new Medico();
                            medico.IdMedico = rd["Id"] != DBNull.Value ? Convert.ToInt32(rd["Id"]) : 0;
                            medico.Nombre = Convert.ToString(rd["Nombre"])!;
                            listadoMedicos.Add(medico);
                        }
                    }
                }
            }
            return listadoMedicos;
        }
    }
}
