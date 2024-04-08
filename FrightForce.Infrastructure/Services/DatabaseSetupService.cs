using FrightForce.Infractructure.Persistence.Utils;
using Npgsql;

namespace FrightForce.Infractructure.Services;

 public class DatabaseSetupService
    {
        //Create Database
        public async Task CreateDatabase(string connectionString, string db)
        {
            connectionString = DatabaseConnectionStringHelper.GetManagementConnectionString(connectionString);

            var database = DatabaseConnectionStringHelper.GetDatabaseName(connectionString);

            await using (NpgsqlConnection connection = new(connectionString))
            {
                await connection.OpenAsync();
                var command = @$"create database {db}
                               with owner = {connection.UserName}
                               encoding = 'UTF8'
                               connection limit = -1;";
                await using (var c = new NpgsqlCommand(command, connection))
                    await c.ExecuteNonQueryAsync();
            }
        }

        //Delete Database
        public async Task DeleteDatabase(string connectionString, string db)
        {
            connectionString = DatabaseConnectionStringHelper.GetManagementConnectionString(connectionString);

            NpgsqlConnection connection = new(connectionString);
            await connection.OpenAsync();

            string command = $@"SELECT pg_terminate_backend(pg_stat_activity.pid)
                               FROM pg_stat_activity
                               WHERE pg_stat_activity.datname = '{db}'";

            await using (var c = new NpgsqlCommand(command, connection))
                await c.ExecuteNonQueryAsync();

            await using (var c = new NpgsqlCommand($"drop  database if exists {db}", connection))
                await c.ExecuteNonQueryAsync();

            await connection.CloseAsync();
        }


        // Source data Load
        public async Task LoadSourceData(string connectionString, string filePath)
        {
            string sqlCommands = await File.ReadAllTextAsync(filePath);
            string[] commands = sqlCommands.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

            await using (NpgsqlConnection connection = new(connectionString))
            {
                await connection.OpenAsync();

                foreach (string commandText in commands)
                {
                    await using (NpgsqlCommand command = new(commandText, connection))
                    {
                        await command.ExecuteNonQueryAsync();
                    }
                }
            }
        }
    }