using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Dapper;
using Npgsql;

namespace ToDo
{
    public interface IPostgresConnection
    {
        IEnumerable<T> ReadData<T>(string statement, object parameters = null);
        void WriteData(string statement, object parameters = null);
    }

    public class PostgresConnection : IPostgresConnection
    {
        private readonly PostgresSettings settings;

        public PostgresConnection(PostgresSettings settings)
        {
            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            this.settings = settings;
            DefaultTypeMap.MatchNamesWithUnderscores = true;
        }

        public IEnumerable<T> ReadData<T>(string statement, object parameters = null)
        {
            IDbConnection connection = null;
            try
            {
                var stopwatch = Stopwatch.StartNew();

                connection = OpenConnection();
                var result = connection.Query<T>(statement, parameters);

                return result;
            }
            catch (NpgsqlException ex)
            {
                if (IsTimeoutException(ex))
                {
                    throw new TimeoutException("Read operation timed out", ex);
                }
                throw;
            }
            finally
            {
                CloseConnection(connection);
            }
        }
      
        public void WriteData(string statement, object parameters = null)
        {
            IDbConnection connection = null;
            try
            {
                var stopwatch = Stopwatch.StartNew();

                connection = OpenConnection();
                connection.Execute(statement, parameters);
            }
            catch (NpgsqlException ex)
            {
                if (IsTimeoutException(ex))
                {
                    throw new TimeoutException("Write operation timed out", ex);
                }
                throw;
            }
            finally
            {
                CloseConnection(connection);
            }
        }

        private IDbConnection BuildConnection()
        {
            var builder = new NpgsqlConnectionStringBuilder
            {
                Host = settings.Host,
                Database = settings.Database,
                Username = settings.Username,
                Password = settings.Password,
                CommandTimeout = settings.CommandTimeoutSeconds,
                MaxPoolSize = settings.MaxPoolSize,
                Timeout = settings.TimeoutSeconds
            };

            return new NpgsqlConnection(builder.ToString());
        }

        private IDbConnection OpenConnection()
        {
            var connection = BuildConnection();
            connection.Open();
            return connection;
        }

        private static void CloseConnection(IDbConnection connection)
        {
            connection?.Dispose();
        }

        private static bool IsTimeoutException(Exception ex)
        {
            return ex.Message == "Exception while reading from stream" && 
                   ex.InnerException != null && 
                   ex.InnerException.Message.Contains("connected party did not properly respond after a period of time");
        }
    }
}