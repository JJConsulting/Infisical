using Microsoft.Extensions.Configuration;

namespace JJConsulting.Infisical.Configuration;

/// <summary>
/// Configuration that authenticates with an Infisical machine identity.
/// </summary>
public sealed class MachineIdentityInfisicalConfig : InfisicalConfig
{
    /// <summary>
    /// Gets the machine identity client identifier.
    /// </summary>
    public string ClientId { get; init; } = null!;

    /// <summary>
    /// Gets the machine identity client secret.
    /// </summary>
    public string ClientSecret { get; init; } = null!;

    /// <summary>
    /// Binds a <see cref="MachineIdentityInfisicalConfig"/> from a configuration section.
    /// </summary>
    /// <param name="section">The configuration section containing Infisical values.</param>
    /// <returns>The bound <see cref="MachineIdentityInfisicalConfig"/> instance.</returns>
    public static MachineIdentityInfisicalConfig FromConfiguration(IConfigurationSection section)
    {
        var config = new MachineIdentityInfisicalConfig();
        section.Bind(config);
        return config;
    }

    /// <inheritdoc />
    public override bool IsValid()
    {
        return !string.IsNullOrWhiteSpace(ClientId) &&
               !string.IsNullOrWhiteSpace(ClientSecret);
    }
}
