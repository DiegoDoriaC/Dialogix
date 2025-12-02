using Microsoft.Data.SqlClient;
using System.Data;

namespace Essalud.Infraestructure.Database
{
    public class SqlConnectionEssaludFactory : ISqlConnectionEssaludFactory
    {
        private readonly string _connectionString;

        public SqlConnectionEssaludFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IDbConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
