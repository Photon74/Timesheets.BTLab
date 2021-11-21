using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Timesheets.DAL.Interfaces;

namespace Timesheets.DAL
{
    public class SQLServerConnectionManager : IDBConnectionManager
    {
        public string ConnectionString => @"Server=localhost\SQLEXPRESS;Database=timesheets;Trusted_Connection=True; Pooling=true";

        public IDbConnection CreateOpenedConnection()
        {
            var connection = new SqlConnection(ConnectionString);
            connection.Open();
            return connection;
        }

    }
}
