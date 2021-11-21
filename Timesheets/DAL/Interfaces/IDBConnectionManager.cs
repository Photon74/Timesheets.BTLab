using System.Data;
using System.Data.SqlClient;

namespace Timesheets.DAL.Interfaces
{
    public interface IDBConnectionManager
    {
        IDbConnection CreateOpenedConnection();
    }
}
