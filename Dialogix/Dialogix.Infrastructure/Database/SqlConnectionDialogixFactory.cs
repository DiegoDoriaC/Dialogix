using Microsoft.Data.SqlClient;
using System.Data;

namespace Dialogix.Infrastructure.Database
{
    public class SqlConnectionDialogixFactory : ISqlConnectionDialogixFactory
    {
        private readonly string _connectionString;

        public SqlConnectionDialogixFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IDbConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
