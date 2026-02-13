using JJConsulting.Infisical.Configuration;
using Microsoft.Extensions.Configuration;

namespace JJConsulting.Infisical.Tests.Configuration;

public class ServiceTokenFromConfigurationTests
{
    [Fact]
    public void FromConfiguration_BindsExpectedValues()
    {
        var data = new Dictionary<string, string?>
        {
            ["Infisical:Environment"] = "staging",
            ["Infisical:ProjectId"] = "workspace-id",
            ["Infisical:ServiceToken"] = "service-token",
            ["Infisical:SecretPath"] = "/api",
            ["Infisical:Url"] = "https://example.infisical.com/api"
        };

        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(data)
            .Build();

        var config = ServiceTokenInfisicalConfig.FromConfiguration(configuration.GetSection("Infisical"));

        Assert.Equal("staging", config.Environment);
        Assert.Equal("workspace-id", config.ProjectId);
        Assert.Equal("service-token", config.ServiceToken);
        Assert.Equal("/api", config.SecretPath);
        Assert.Equal("https://example.infisical.com", config.Url);
    }
}
