using FrightForce.Infractructure.Persistence;
using FrightForce.Infractructure.Persistence.Utils;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FrightForce.Infractructure.Services;

public class DatabaseInitializerService
    {
        public async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                Console.WriteLine("BaseDir : " + AppDomain.CurrentDomain.BaseDirectory);

                var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();
                var contextInitializer = scope.ServiceProvider.GetRequiredService<FrightForceDbContextInitializer>();
                var setUpService = scope.ServiceProvider.GetRequiredService<DatabaseSetupService>();


                string defaultConnectionString = configuration.GetConnectionString("DefaultConnection");
                var localConnectionString =
                    DatabaseConnectionStringHelper.BuildConnectionEnvBasedConnectionString(defaultConnectionString);

                var connectionString = localConnectionString ?? defaultConnectionString;
                string databaseName = null;

                //Split Database name from connection string
                var parameters = connectionString.Split(';');
                foreach (var param in parameters)
                {
                    if (param.StartsWith("Database=", StringComparison.OrdinalIgnoreCase))
                    {
                        databaseName = param.Split('=')[1];
                        break;
                    }
                }

                if (databaseName != null)
                {
                    // Delete Database
                    await setUpService.DeleteDatabase(connectionString, databaseName);

                    // Create Database
                    await setUpService.CreateDatabase(connectionString, databaseName);

                    // Update Migrations
                    await contextInitializer.InitialiseAsync();

                    // Seed Data
                    await contextInitializer.SeedAsync();

                    // Load Source Data
                    var scriptPath = Path.Combine(
                        AppDomain.CurrentDomain.BaseDirectory,
                        "..",
                        "..",
                        "..",
                        "..",
                        "FrightForce.Infrastructure",
                        "Utils",
                        "Scripts",
                        "Sql",
                        "source_data.sql");

                    await setUpService.LoadSourceData(connectionString, scriptPath);
                }
                else
                {
                    // Handle error
                    Console.WriteLine("Error: Database name not found in connection string.");
                }
            }
        }
    }