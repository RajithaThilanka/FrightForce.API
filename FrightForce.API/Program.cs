
using FrightForce.API;
using FrightForce.Application;
using FrightForce.Infractructure;
using FrightForce.Infractructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
ConfigureEnvironment(builder);
ConfigureLogging(builder);

ConfigureServices(builder);

var app = builder.Build();

await ConfigurePipeline();
app.Run();

#region Configuration Definitions

void ConfigureServices(WebApplicationBuilder builder)
{
    builder.Services.AddControllers();

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services.AddInfrastructureServices(builder);
    builder.Services.AddApplicationServices();
    builder.Services.AddApiServices();

    builder.Services.AddHealthChecks();
    builder.Services.AddCors(option =>
    {
        option.AddPolicy("allowedOrigin",
            builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()
        );
    });
}


async Task ConfigurePipeline()
{
    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();

        await ConfigureDbInitializer();

        app.UseExceptionHandler("/error-development");
    }
    else
    {
        app.UseExceptionHandler("/error");
    }

    app.UseMiddleware<GlobalExceptionHandlerMiddleware>();
    app.UseCors("allowedOrigin");
    app.UseHttpsRedirection();

    //app.UseMiddleware<TenantResolver>();

    app.UseAuthentication();
    app.UseAuthorization();

    //app.UseApiPipelines();

    app.MapControllers();
    app.MapHealthChecks("/health-check");
    app.Run();
}

async Task ConfigureDbInitializer()
{
    var runOnStartup = builder.Configuration.GetValue<bool>("DatabaseInitializer:RunOnStartUp");

    if (runOnStartup || DotNetEnv.Env.GetBool("INITIALIZE_DB_ON_STARTUP"))
    {
        using var scope = app.Services.CreateScope();
        var databaseInitializer = scope.ServiceProvider.GetRequiredService<DatabaseInitializerService>();
        await databaseInitializer.InitializeAsync(scope.ServiceProvider);
        //Auto restore database 
    }
}

void ConfigureLogging(WebApplicationBuilder wab)
{
    // Serilog self logging
    if (DotNetEnv.Env.GetBool("SERILOG_SELF_LOG"))
    {
        //Serilog.Debugging.SelfLog.Enable(Console.WriteLine);
    }

    // load main serilog configuration
    //wab.Host.UseSerilog((context, configuration) => { configuration.ReadFrom.Configuration(context.Configuration); });
}

void ConfigureEnvironment(WebApplicationBuilder wab)
{
    // load environment variables from all the .env files
    if (wab.Environment.IsDevelopment())
    {
        DotNetEnv.Env.TraversePath().Load();
    }
}
public partial class Program { }
#endregion


