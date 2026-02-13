using JJConsulting.Infisical.Services;
using Microsoft.Extensions.Configuration;

namespace JJConsulting.Infisical.Configuration;

public sealed class ServiceTokenInfisicalConfig : InfisicalConfig
{
    public string ServiceToken { get; init; } = null!;

    public static ServiceTokenInfisicalConfig FromConfiguration(IConfigurationSection section)
    {
        var config = new ServiceTokenInfisicalConfig();
        section.Bind(config);
        return config;
    }

    public override bool IsValid()
    {
        return !string.IsNullOrWhiteSpace(ServiceToken);
    }

    public override IInfisicalAuthenticationService CreateAuthenticationService()
    {
        return new ServiceTokenAuthenticationService(this);
    }
}
