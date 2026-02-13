using JJConsulting.Infisical.Configuration;
using JJConsulting.Infisical.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace JJConsulting.Infisical.Tests.Configuration;

public class InfisicalConfigurationExtensionsTests
{
    [Fact]
    public async Task AddInfisical_OnServiceCollection_RegistersServicesAndConfig()
    {
        var config = new ServiceTokenInfisicalConfig
        {
            Environment = "prod",
            ProjectId = "project-id",
            ServiceToken = "service-token"
        };

        var services = new ServiceCollection();

        var returned = services.AddInfisical(config);

        Assert.Same(services, returned);

        using var provider = services.BuildServiceProvider();

        Assert.Same(config, provider.GetRequiredService<InfisicalConfig>());
        Assert.Same(config, provider.GetRequiredService<IOptions<InfisicalConfig>>().Value);

        var authenticationService = provider.GetRequiredService<IInfisicalAuthenticationService>();
        var token = await authenticationService.GetBearerTokenAsync();
        Assert.Equal("service-token", token);

        var firstSecretsService = provider.GetRequiredService<IInfisicalSecretsService>();
        var secondSecretsService = provider.GetRequiredService<IInfisicalSecretsService>();
        Assert.NotNull(firstSecretsService);
        Assert.NotNull(secondSecretsService);
        Assert.NotSame(firstSecretsService, secondSecretsService);
    }

    [Fact]
    public void AddInfisical_OnConfigurationBuilder_AddsSourceAndReturnsBuilder()
    {
        var config = new ServiceTokenInfisicalConfig
        {
            Environment = "prod",
            ProjectId = "project-id",
            ServiceToken = "service-token"
        };

        var builder = new ConfigurationBuilder();

        var returned = builder.AddInfisical(config);

        Assert.Same(builder, returned);
        var source = Assert.Single(builder.Sources);
        Assert.Equal("InfisicalConfigurationSource", source.GetType().Name);
    }
}
