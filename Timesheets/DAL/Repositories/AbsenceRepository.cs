using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Timesheets.DAL.Entitys;
using Timesheets.DAL.Interfaces;

namespace Timesheets.DAL.Repositories
{
    public class AbsenceRepository : IAbsenceRepository
    {
        private readonly IDBConnectionManager _connection;

        public AbsenceRepository(IDBConnectionManager connection)
        {
            _connection = connection;
        }

        public async Task CreateAsync(AbsenceEntity entity)
        {
            using var connection = _connection.CreateOpenedConnection() as SqlConnection;
            var cmd = connection.CreateCommand();

            cmd.CommandText = $"INSERT INTO absence (reason, start_date, duration, discounted, description) " +
                $"VALUES ({entity.Reason}, " +
                $"'{entity.StartDate:dd-MM-yyyy}', " +
                $"{entity.Duration}, " +
                $"{entity.Discounted}, " +
                $"'{entity.Description}')";

            await cmd.ExecuteNonQueryAsync();
        }

        public async Task DeleteAsync(int id)
        {
            using var connection = _connection.CreateOpenedConnection() as SqlConnection;
            var cmd = connection.CreateCommand();

            cmd.CommandText = $"DELETE FROM absence WHERE id = {id}";

            await cmd.ExecuteNonQueryAsync();
        }

        public async Task<AbsenceEntity> GetByIdAsync(int id)
        {
            var result = new AbsenceEntity();
            using var connection = _connection.CreateOpenedConnection() as SqlConnection;
            var cmd = connection.CreateCommand();
            cmd.CommandText = $"SELECT * FROM absence WHERE id = {id}";

            using var reader = await cmd.ExecuteReaderAsync();

            await reader.ReadAsync();

            result.Id = reader.GetInt32(0);
            result.Reason = reader.GetInt32(1);
            result.StartDate = reader.GetDateTime(2);
            result.Duration = reader.GetInt32(3);
            result.Discounted = reader.GetBoolean(4) == false ? 0 : 1;
            result.Description = reader.GetString(5);

            return result;
        }

        public async Task<IList<AbsenceEntity>> GetAllAsync()
        {
            using var connection = _connection.CreateOpenedConnection() as SqlConnection;
            var cmd = connection.CreateCommand();
            cmd.CommandText = $"SELECT * FROM absence";

            using var reader = await cmd.ExecuteReaderAsync();

            var responce = new List<AbsenceEntity>();

            while (reader.Read())
            {
                var a = new AbsenceEntity
                {
                    Id = reader.GetInt32(0),
                    Reason = reader.GetInt32(1),
                    StartDate = reader.GetDateTime(2),
                    Duration = reader.GetInt32(3),
                    Discounted = reader.GetBoolean(4) == false ? 0 : 1,
                    Description = reader.GetString(5)
                };
                responce.Add(a);
            }
            return responce;
        }

        public async Task UpdateAsync(int id, AbsenceEntity entity)
        {
            using var connection = _connection.CreateOpenedConnection() as SqlConnection;
            var cmd = connection.CreateCommand();

            cmd.CommandText = $"UPDATE absence " +
                $"SET " +
                $"reason = {entity.Reason}, " +
                $"start_date = '{entity.StartDate:dd-MM-yyyy}', " +
                $"duration = {entity.Duration}, " +
                $"discounted = {entity.Discounted}, " +
                $"description = '{entity.Description}' " +
                $"WHERE id = {id}";

            await cmd.ExecuteNonQueryAsync();
        }
    }
}
