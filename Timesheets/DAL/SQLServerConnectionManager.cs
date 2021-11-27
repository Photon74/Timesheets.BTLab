using System;
using System.Data;
using System.Data.SqlClient;
using Timesheets.DAL.Interfaces;

namespace Timesheets.DAL
{
    public class SQLServerConnectionManager : IDBConnectionManager
    {
        private static SqlConnection conn = null;
        private static SqlCommand cmd = null;
        private static string sql = null;

        public string ConnectionString => @"Server=localhost\SQLEXPRESS;Database=timesheets;Trusted_Connection=True; Pooling=true";

        public IDbConnection CreateOpenedConnection()
        {
            var connection = new SqlConnection(ConnectionString);
            connection.Open();
            return connection;
        }

        internal static bool CheckDatabase(string databaseName)
        {
            var connectionString = @"Server=localhost\SQLEXPRESS;Database=master;Trusted_Connection=True;";
            var cmdText = "select count(*) from master.dbo.sysdatabases where name=@database";

            using var connection = new SqlConnection(connectionString);
            using var cmd = new SqlCommand(cmdText, connection);

            cmd.Parameters.Add("@database", System.Data.SqlDbType.NVarChar).Value = databaseName;
            connection.Open();
            return Convert.ToInt32(cmd.ExecuteScalar()) == 1;
        }

        internal static void CreateDatabase()
        {
            // Create a connection  
            conn = new SqlConnection(@"Server = localhost\SQLEXPRESS; Database = master; Trusted_Connection = True; ");
            // Open the connection  
            if (conn.State != ConnectionState.Open)
                conn.Open();
            string sql = @"CREATE DATABASE [timesheets] CONTAINMENT = NONE ON PRIMARY(NAME = N'timesheets',FILENAME = N'D:\TestWork\Timesheets\timesheets.mdf' , SIZE = 8192KB , FILEGROWTH = 65536KB ) LOG ON(NAME = N'timesheets_log', FILENAME = N'D:\TestWork\Timesheets\timesheets_log.ldf' , SIZE = 8192KB , FILEGROWTH = 65536KB )";
            ExecuteSQLStmt(sql);
            CreateTable();
        }

        private static void ExecuteSQLStmt(string sql)
        {
            if (conn.State == ConnectionState.Open)
                conn.Close();
            var ConnectionString = @"Server=localhost\SQLEXPRESS;Database=master;Trusted_Connection=True;";
            conn.ConnectionString = ConnectionString;
            conn.Open();
            cmd = new SqlCommand(sql, conn);
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                throw new Exception(e.Message);
            }
        }

        private static void CreateTable()
        {
            // Open the connection  
            if (conn.State == ConnectionState.Open)
                conn.Close();
            var ConnectionString = @"Server=localhost\SQLEXPRESS;Database=timesheets;Trusted_Connection=True; Pooling=true";
            conn.ConnectionString = ConnectionString;
            conn.Open();
            sql = @"CREATE TABLE [dbo].[absence]
                    (
                    	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
                        [reason] INT NOT NULL, 
                        [start_date] DATETIME NOT NULL, 
                        [duration] INT NOT NULL, 
                        [discounted] BIT NOT NULL, 
                        [description] NVARCHAR(1024) NOT NULL
                    )";
            cmd = new SqlCommand(sql, conn);
            try
            {
                cmd.ExecuteNonQuery();
                // Adding records the table  
                sql = "INSERT INTO absence (reason, start_date, duration, discounted, description) " +
                "VALUES (1, '20.11.2021', 5, 1, 'ПЕРВЫЙ') ";
                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();

                sql = "INSERT INTO absence (reason, start_date, duration, discounted, description) " +
                "VALUES (2, '21.11.2021', 3, 1, 'ВТОРОЙ') ";
                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();

                sql = "INSERT INTO absence (reason, start_date, duration, discounted, description) " +
                "VALUES (1, '23.11.2021', 4, 0, 'ТРЕТИЙ') ";
                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();

                sql = "INSERT INTO absence (reason, start_date, duration, discounted, description) " +
                "VALUES (0, '20.11.2021', 12, 1, 'ЧЕТВЕРТЫЙ') ";
                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
