using N2.DelegateHandlers.Brokers;
using N2.DelegateHandlers.DelegatingHandlers;
using N2.DelegateHandlers.Settings;

namespace N2.DelegateHandlers.Configurations;

public static partial class HostConfiguration
{
    private static WebApplicationBuilder AddLogging(this WebApplicationBuilder builder)
    {
        // add console logger
        builder.Services.AddLogging();

        return builder;
    }

    /// <summary>
    /// Configures exposers including controllers
    /// </summary>
    /// <param name="builder">Application builder</param>
    /// <returns></returns>
    private static WebApplicationBuilder AddExposers(this WebApplicationBuilder builder)
    {
        builder.Services.AddRouting(options => options.LowercaseUrls = true);
        builder.Services.AddControllers();

        return builder;
    }

    public static WebApplicationBuilder AddIdentityInfrastructure(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<IdentityApiSettings>(builder.Configuration.GetSection(nameof(IdentityApiSettings)));

        builder.Services.AddMemoryCache();
        builder.Services.AddTransient<LoggingHandler>().AddTransient<AuthenticationHandler>().AddTransient<CachingHandler>();

        builder.Services.AddHttpClient();

        builder.Services
            .AddHttpClient<IIdentityApiBroker, IdentityApiBroker>(
                client => { client.BaseAddress = new Uri(builder.Configuration["IdentityApiSettings:BaseAddress"]!); }
            )
            .AddHttpMessageHandler<LoggingHandler>()
            .AddHttpMessageHandler<CachingHandler>()
            .AddHttpMessageHandler<AuthenticationHandler>();

        return builder;
    }

    /// <summary>
    /// Configures devTools including controllers
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    private static WebApplicationBuilder AddDevTools(this WebApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        return builder;
    }

    /// <summary>
    /// Add Controller middleWhere
    /// </summary>
    /// <param name="app">Application host</param>
    /// <returns>Application host</returns>
    private static WebApplication UseExposers(this WebApplication app)
    {
        app.MapControllers();

        return app;
    }

    private static WebApplication UseDevTools(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();

        return app;
    }

    private static WebApplication UseAuth(this WebApplication app)
    {
        app.UseAuthentication();
        app.UseAuthorization();

        return app;
    }
}