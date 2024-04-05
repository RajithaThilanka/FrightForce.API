using FrightForce.Domain.Base;
using FrightForce.Domain.Common.Services;
using FrightForce.Domain.Identity;
using FrightForce.Infractructure.CAP;
using FrightForce.Infractructure.FileStorage;
using FrightForce.Infractructure.Persistence;
using FrightForce.Infractructure.Persistence.Configurations;
using FrightForce.Infractructure.Persistence.Interceptors;
using FrightForce.Infractructure.Services;
using FrightForce.Infractructure.Utils.Json;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.TokenCacheProviders.InMemory;
using Microsoft.IdentityModel.Logging;

namespace FrightForce.Infractructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
    WebApplicationBuilder builder)
    {
        services.AddPersistenceContext(builder.Configuration);
        // services.AddIdentityServices(builder.Configuration);
        services.AddGraphApiServices(builder.Configuration);
        services.AddCommonInfraServices();
        services.AddEfCoreInterceptors();
        services.AddRepositories();
        services.AddClients();

        services.AddCapMessaging();
        services.AddInfrastructureUtils();
        return services;
    }

    private static IServiceCollection AddPersistenceContext(this IServiceCollection services,
        IConfiguration configuration)
    {
        // Enable legacy timestamp behavior
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        services.AddDbContext<FrightForceDbContext>(options =>
        {
            options.UseNpgsql(
                configuration.GetConnectionString("DefaultConnection"), builder =>
                {
                    builder.MigrationsHistoryTable(MigrationsDefinitions.MigrationsHistoryTableName,
                        FrightForceDbContext.DefaultSchema);
                    builder.MigrationsAssembly(typeof(FrightForceDbContext).Assembly.GetName().Name);
                }
            );
        });

        services.AddScoped<ITransactionContextAwareDbContext>(provider => provider.GetService<FrightForceDbContext>());

        services.AddScoped<FrightForceDbContextInitializer>();
        services.AddSingleton<DatabaseSetupService>();
        services.AddScoped<DatabaseInitializerService>();
        services.Configure<FileClientConnectionStrings>(
            (settings) =>
            {
                settings.BlobConnectionString = configuration["ClientConnectionStrings:BlobConnectionString"];
                settings.AccountKey = configuration["ClientConnectionStrings:AccountKey"];
                settings.AccountName = configuration["ClientConnectionStrings:AccountName"];
            }
        );


        return services;
    }


    private static IServiceCollection AddCommonInfraServices(this IServiceCollection services)
    {
        services.AddSingleton<IDateTime, DateTimeService>();
        services.AddScoped<ICurrentUserService, CurrentUserService>();

        return services;
    }


    private static IServiceCollection AddEfCoreInterceptors(this IServiceCollection services)
    {
        services.AddTransient<AuditableEntitySaveChangesInterceptor>();

        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        // services.AddScoped<IDocumentTypeRepository, DocumentTypeRepository>();
        // services.AddScoped<IDocketRepository, DocketRepository>();
        // services.AddScoped<IDocumentRepository, DocumentRepository>();  
        return services;
    }

    private static IServiceCollection AddIdentityServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddHttpContextAccessor();
        IdentityModelEventSource.ShowPII = true;
        services.AddIdentity<ApplicationUser, ApplicationRole>(
                options =>
                {
                    options.User.RequireUniqueEmail = true;
                    options.SignIn.RequireConfirmedEmail = true;
                    options.SignIn.RequireConfirmedAccount = true;
                    options.Password.RequireDigit = true;
                    options.Password.RequireLowercase = true;
                    options.Password.RequireUppercase = true;
                    options.Password.RequireNonAlphanumeric = true;
                    options.Password.RequiredLength = 8;
                    options.Password.RequiredUniqueChars = 1;
                }
            )
            .AddEntityFrameworkStores<FrightForceDbContext>()
            .AddDefaultTokenProviders();


        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })
            .AddMicrosoftIdentityWebApi(configuration.GetSection("AzureAd"))
            .EnableTokenAcquisitionToCallDownstreamApi();

        services.AddTransient<IClaimsTransformation, AppClaimsTransformer>();

        return services;
    }

    private static IServiceCollection AddGraphApiServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddMicrosoftGraph(options =>
        {
            configuration.GetSection("DownstreamApis:MicrosoftGraph").Bind(options);
        }
        ).AddInMemoryTokenCaches();

        return services;
    }

    private static IServiceCollection AddClients(this IServiceCollection services)
    {
        //CAP
        services.AddTransient<IBusPublisher, BusPublisher>();
        services.AddTransient<IEventMapper, EventMapper>();
        //services.AddTransient<IStorageClient, AzureBlobClient>();
        //services.AddTransient<IStorageProvider, StorageProvider>();

        return services;
    }

    private static IServiceCollection AddInfrastructureUtils(this IServiceCollection services)
    {
        services.AddSingleton<IJsonUtils, JsonUtils>();

        return services;
    }

}

