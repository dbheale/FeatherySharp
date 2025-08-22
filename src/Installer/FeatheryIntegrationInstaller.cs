using System.Net.Http.Headers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace Feathery.Unofficial.Client.Installer;

/// <summary>
/// Adds the Feathery Client into your DI container for you.
/// </summary>
public static class FeatheryIntegrationInstaller
{
    /// <summary>
    /// Adds Feathery client/services using the given builder’s Configuration/Services.
    /// Looks for configuration in section "Feathery" by default:
    /// 
    ///  "Feathery": {
    ///    "ApiKey": "...",
    ///    "BaseUri": "https://api.feathery.io/",
    ///    "PdfPollAttempts": 5,
    ///    "PdfPollDelay": "00:00:01"
    ///  }
    /// 
    /// Also supports env var fallback: FEATHERY_API_KEY.
    /// </summary>
    /// <param name="builder">IHostApplicationBuilder</param>
    /// <param name="sectionName">Section name for the Feathery configuration</param>
    /// <param name="configure">Action to alter the feathery options</param>
    /// <returns>IServiceCollection</returns>
    public static IHostApplicationBuilder AddFeathery(
        this IHostApplicationBuilder builder,
        string sectionName = "Feathery",
        Action<FeatheryOptions>? configure = null)
    {
        AddFeathery(builder.Services, builder.Configuration, sectionName, configure);
        return builder;
    }

    /// <summary>
    /// Adds Feathery client/services using the given builder’s Configuration/Services.
    /// Looks for configuration in section "Feathery" by default:
    /// 
    ///  "Feathery": {
    ///    "ApiKey": "...",
    ///    "BaseUri": "https://api.feathery.io/",
    ///    "PdfPollAttempts": 5,
    ///    "PdfPollDelay": "00:00:01"
    ///  }
    /// 
    /// Also supports env var fallback: FEATHERY_API_KEY.
    /// </summary>
    /// <param name="services">IServiceCollection</param>
    /// <param name="configuration">IConfiguration</param>
    /// <param name="sectionName">Section name for the Feathery configuration</param>
    /// <param name="configure">Action to alter the feathery options</param>
    /// <returns>IServiceCollection</returns>
    public static IServiceCollection AddFeathery(
        this IServiceCollection services,
        IConfiguration configuration,
        string sectionName = "Feathery",
        Action<FeatheryOptions>? configure = null)
    {
        var section = configuration.GetSection(sectionName);

        services.AddOptions<FeatheryOptions>()
            .Bind(section)
            .PostConfigure(opts =>
            {
                // Fallback to environment variable if ApiKey is missing.
                if (string.IsNullOrWhiteSpace(opts.ApiKey))
                    opts.ApiKey = Environment.GetEnvironmentVariable("FEATHERY_API_KEY") ?? opts.ApiKey;

                var baseStr = opts.BaseUri.ToString();
                if (!baseStr.EndsWith("/", StringComparison.Ordinal))
                    opts.BaseUri = new Uri(baseStr + "/");

                configure?.Invoke(opts);
            })
            .Validate(o => !string.IsNullOrWhiteSpace(o.ApiKey),
                $"Feathery ApiKey is required. Set `{sectionName}:ApiKey` or the FEATHERY_API_KEY environment variable.")
            .Validate(o => o.BaseUri is not null, "Feathery BaseUri is required.")
            .ValidateOnStart();

        // I hate injecting IOptions<T>, let's just add the singleton of the value.
        services.AddSingleton(sp => sp.GetRequiredService<IOptions<FeatheryOptions>>().Value);

        // Register typed HttpClient + client implementation.
        services.AddHttpClient<IFeatheryClient, FeatheryClient>((sp, http) =>
        {
            var opts = sp.GetRequiredService<IOptions<FeatheryOptions>>().Value;

            http.BaseAddress = opts.BaseUri;
            // Feathery expects: Authorization: Token <API KEY>
            http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Token", opts.ApiKey);
        });

        return services;
    }
}