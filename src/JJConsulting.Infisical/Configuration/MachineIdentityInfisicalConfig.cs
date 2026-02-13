using Microsoft.Extensions.Configuration;

namespace JJConsulting.Infisical.Configuration;

public sealed class MachineIdentityInfisicalConfig : InfisicalConfig
{
    public string ClientId { get; init; } = null!;
    public string ClientSecret { get; init; } = null!;

    public static MachineIdentityInfisicalConfig FromConfiguration(IConfigurationSection section)
    {
        var config = new MachineIdentityInfisicalConfig();
        section.Bind(config);
        return config;
    }

    public override bool IsValid()
    {
        return !string.IsNullOrWhiteSpace(ClientId) &&
               !string.IsNullOrWhiteSpace(ClientSecret);
    }
}
