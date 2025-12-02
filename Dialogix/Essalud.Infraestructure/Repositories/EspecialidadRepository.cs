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
    public class EspecialidadRepository : IEspecialidadRepository
    {
        private readonly ISqlConnectionEssaludFactory _connectionFactory;

        public EspecialidadRepository(ISqlConnectionEssaludFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<List<string>> ListarEspecialidades()
        {
            List<string> listadoCitas = new List<string>();

            using (var connection = (SqlConnection)_connectionFactory.CreateConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "pr_listar_especialidades";
                    command.CommandType = CommandType.StoredProcedure;

                    await connection.OpenAsync();

                    using (var rd = await command.ExecuteReaderAsync())
                    {
                        while (await rd.ReadAsync())
                        {
                            string especialidad = Convert.ToString(rd["Especialidad"])!;
                            listadoCitas.Add(especialidad);
                        }
                    }
                }
            }
            return listadoCitas;
        }
    }
}
