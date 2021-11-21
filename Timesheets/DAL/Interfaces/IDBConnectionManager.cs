using System.Data;

namespace Timesheets.DAL.Interfaces
{
    public interface IDBConnectionManager
    {
        IDbConnection CreateOpenedConnection();
    }
}
