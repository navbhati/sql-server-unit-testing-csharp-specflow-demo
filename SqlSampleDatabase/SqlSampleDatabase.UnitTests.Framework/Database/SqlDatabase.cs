using Dapper;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using SqlSampleDatabase.UnitTests.Framework.Helpers;

namespace SqlSampleDatabase.UnitTests.Framework.Database
{
    public class SqlDatabase : IDisposable
    {
        private readonly IDbConnection _connection;
        private readonly int _sqlCommandTimeoutSeconds;

        public SqlDatabase(string connectionString, int sqlCommandTimeoutSeconds)
        {
            _connection = new SqlConnection(connectionString);
            _sqlCommandTimeoutSeconds = sqlCommandTimeoutSeconds;
        }

        public async Task InsertAsync(string databaseName, string tableName, TypedTable table)
        {
            await InsertAsync(databaseName, tableName, table.ToDictionaries());
        }

        public async Task InsertAsync(string databaseName, string name, IEnumerable<Dictionary<string, object>> rows)
        {
            foreach (var row in rows)
            {
                var columns = string.Join(",", row.Keys);
                var values = string.Join(",", row.Keys.Select(k => $"@{k}"));
                var sql = $"INSERT INTO {name} ({columns}) VALUES ({values})";
                try
                {
                    await ExecuteCommandAsync(databaseName, sql, row);
                }
                catch
                {
                    TestContext.WriteLine($"Insert SQL Error Table {name}: {sql}");
                    throw;
                }
            }
        }

        public async Task TruncateAsync(string databaseName, string tableName)
        {
            try
            {
                await ExecuteCommandAsync(databaseName, $"TRUNCATE TABLE {tableName}");
            }
            catch (SqlException sqlEx)
            {
                if (!sqlEx.Message.Contains("because it is not a table"))
                    throw;

                await ExecuteCommandAsync(databaseName, $"DELETE FROM {tableName}");
            }
        }

        public async Task<IEnumerable<IDictionary<string, object>>> ExecProcAsync(string databaseName, string procName, Table parameters)
        {
            return await ExecProcAsync(databaseName, procName, parameters.ToDictionary());
        }

        public async Task<IEnumerable<IDictionary<string, object>>> ReadAllAsync(string databaseName, string tableName)
        {
            var sql = $"SELECT * FROM {tableName}";
            return (await GetConnection(databaseName).QueryAsync(sql)).Select(d => (IDictionary<string, object>)d);
        }

        private Task<int> ExecuteCommandAsync(string databaseName, string sql, IDictionary<string, object> rowData)
        {
            return GetConnection(databaseName).ExecuteAsync(sql, rowData, commandTimeout: _sqlCommandTimeoutSeconds);
        }

        private Task<int> ExecuteCommandAsync(string databaseName, string sql, SqlMapper.IDynamicParameters parameters = null)
        {
            return parameters != null
                ? GetConnection(databaseName).ExecuteAsync(sql, parameters, commandTimeout: _sqlCommandTimeoutSeconds)
                : GetConnection(databaseName).ExecuteAsync(sql, commandTimeout: _sqlCommandTimeoutSeconds);
        }

        private async Task<IEnumerable<IDictionary<string, object>>> ExecProcAsync(string databaseName, string procName, IDictionary<string, object> parameters)
        {
            return (await GetConnection(databaseName).QueryAsync(procName, parameters, commandType: CommandType.StoredProcedure)).Select(d => (IDictionary<string, object>)d);
        }

        private void OpenConnection()
        {
            if (_connection.State != ConnectionState.Closed) return;

            try
            {
                TestContext.WriteLine($"Connecting to SQL Server: '{_connection.ConnectionString}'");
                _connection.Open();
            }
            catch
            {
                TestContext.Error.WriteLine($"Failed to connect to SQL Server: '{_connection.ConnectionString}'");
                throw;
            }
        }

        private IDbConnection GetConnection(string databaseName)
        {
            OpenConnection();
            if (string.Compare(_connection.Database, databaseName, StringComparison.OrdinalIgnoreCase) == 0) return _connection;

            try
            {
                TestContext.WriteLine($"Switching to database: '{databaseName}'");
                _connection.ChangeDatabase(databaseName);
                return _connection;
            }
            catch
            {
                TestContext.Error.WriteLine($"Unable to switch to database: '{databaseName}'");
                throw;
            }
        }

        public void Dispose()
        {
            _connection?.Dispose();
        }
    }
}
