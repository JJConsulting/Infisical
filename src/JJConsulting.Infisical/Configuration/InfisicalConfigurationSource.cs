using JJConsulting.Infisical.Services;
using Microsoft.Extensions.Configuration;

namespace JJConsulting.Infisical.Configuration;

internal sealed class InfisicalConfigurationSource(
    InfisicalConfig config,
    Func<IInfisicalAuthenticationService> authenticationServiceFactory) : IConfigurationSource
{
    public IConfigurationProvider Build(IConfigurationBuilder builder)
    {
        return new InfisicalConfigurationProvider(config, authenticationServiceFactory());
    }
}
