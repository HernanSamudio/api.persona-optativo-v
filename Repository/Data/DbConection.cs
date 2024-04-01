using System.Data;
using Microsoft.Data.SqlClient;

namespace Repository.Data
{
    public class DbConection
    {
        private readonly string _connectionString;

        public DbConection(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IDbConnection CreateConnection()
        {
            try
            {
                IDbConnection connection = new SqlConnection(_connectionString);
                connection.Open();
                return connection;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error al abrir la conexión con la base de datos", ex);
            }
        }
    }
}
