using System.Data.Common;
using System.Text.RegularExpressions;
using Npgsql;

namespace FrightForce.Infractructure.Persistence.Utils;

public static class DatabaseConnectionStringHelper
{
    public static string GetDatabaseName(string connectionString)
    {
        DbConnectionStringBuilder builder = new DbConnectionStringBuilder();
        builder.ConnectionString = connectionString;

        var database = builder.TryGetValue("Database", out var value) ? value.ToString() : "frightForce";
        if (database == null)
        {
            throw new ArgumentException("Database Name Could Not Be Found in Connection String.");
        }

        return database;
    }

    public static string GetManagementConnectionString(string connectionString)
    {
        string pattern = @"Database=\w+";
        string replacement = "Database=postgres";
        return Regex.Replace(connectionString, pattern, replacement);
    }

    public static string? BuildConnectionEnvBasedConnectionString(string defaultConnString, bool isManagement = false)
    {
        var dbServer = Environment.GetEnvironmentVariable("DEV_DB_SERVER");
        var dbDatabase = Environment.GetEnvironmentVariable("DEV_DB");
        var dbUser = Environment.GetEnvironmentVariable("DEV_DB_USER");
        var dbPassword = Environment.GetEnvironmentVariable("DEV_DB_PASSWORD");

        if (!string.IsNullOrWhiteSpace(dbServer) &&
            !string.IsNullOrWhiteSpace(dbDatabase) &&
            !string.IsNullOrWhiteSpace(dbUser) &&
            !string.IsNullOrWhiteSpace(dbPassword))
        {
            var builder = new NpgsqlConnectionStringBuilder(defaultConnString)
            {
                Host = dbServer,
                Database = dbDatabase,
                Username = dbUser,
                Password = dbPassword
            };

            return builder.ToString();
        }

        return null;
    }
}