using Microsoft.Extensions.Configuration;

namespace JJConsulting.Infisical.Configuration;

internal sealed class InfisicalConfigurationSource(InfisicalConfig config) : IConfigurationSource
{
    public IConfigurationProvider Build(IConfigurationBuilder builder)
    {
        return new InfisicalConfigurationProvider(config);
    }
}