using Microsoft.Extensions.Configuration;

namespace JJConsulting.Infisical.Configuration;

/// <summary>
/// Configuration that authenticates with an Infisical service token.
/// </summary>
public sealed class ServiceTokenInfisicalConfig : InfisicalConfig
{
    /// <summary>
    /// Gets the service token used to authenticate requests.
    /// </summary>
    public string ServiceToken { get; init; } = null!;

    /// <summary>
    /// Binds a <see cref="ServiceTokenInfisicalConfig"/> from a configuration section.
    /// </summary>
    /// <param name="section">The configuration section containing Infisical values.</param>
    /// <returns>The bound <see cref="ServiceTokenInfisicalConfig"/> instance.</returns>
    public static ServiceTokenInfisicalConfig FromConfiguration(IConfigurationSection section)
    {
        var config = new ServiceTokenInfisicalConfig();
        section.Bind(config);
        return config;
    }

    /// <inheritdoc />
    public override bool IsValid()
    {
        return !string.IsNullOrWhiteSpace(ServiceToken);
    }
}
