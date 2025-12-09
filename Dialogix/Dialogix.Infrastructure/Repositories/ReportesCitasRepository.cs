using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Essalud.Infraestructure.Database;


namespace Dialogix.Infrastructure.Repositories
{
    public class ReportesCitasRepository : IReportesCitasRepository
    {
        private readonly ISqlConnectionEssaludFactory _connectionFactory;

        public ReportesCitasRepository(ISqlConnectionEssaludFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<int> ObtenerTotalCitasAgendadas()
        {
            int total = 0;

            using (var connection = (SqlConnection)_connectionFactory.CreateConnection())
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "pr_total_citas_agendadas";
                command.CommandType = CommandType.StoredProcedure;

                await connection.OpenAsync();

                using (var rd = await command.ExecuteReaderAsync())
                {
                    if (await rd.ReadAsync())
                    {
                        total = Convert.ToInt32(rd["TotalCitas"]);
                    }
                }
            }

            return total;
        }
    }

}
