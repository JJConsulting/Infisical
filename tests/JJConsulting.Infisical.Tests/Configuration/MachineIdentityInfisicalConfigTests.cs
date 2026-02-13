using JJConsulting.Infisical.Configuration;
using Microsoft.Extensions.Configuration;

namespace JJConsulting.Infisical.Tests.Configuration;

public class MachineIdentityInfisicalConfigTests
{
    [Fact]
    public void IsValid_ReturnsTrue_WhenClientIdAndClientSecretPresent()
    {
        var config = new MachineIdentityInfisicalConfig
        {
            Environment = "prod",
            ProjectId = "project-id",
            ClientId = "client-id",
            ClientSecret = "client-secret"
        };

        Assert.True(config.IsValid());
    }

    [Theory]
    [InlineData(null, "client-secret")]
    [InlineData("", "client-secret")]
    [InlineData(" ", "client-secret")]
    [InlineData("client-id", null)]
    [InlineData("client-id", "")]
    [InlineData("client-id", " ")]
    public void IsValid_ReturnsFalse_WhenClientIdOrClientSecretMissing(string? clientId, string? clientSecret)
    {
        var config = new MachineIdentityInfisicalConfig
        {
            Environment = "prod",
            ProjectId = "project-id",
            ClientId = clientId!,
            ClientSecret = clientSecret!
        };

        Assert.False(config.IsValid());
    }

    [Fact]
    public void FromConfiguration_BindsExpectedValues()
    {
        var data = new Dictionary<string, string?>
        {
            ["Infisical:Environment"] = "dev",
            ["Infisical:ProjectId"] = "workspace-id",
            ["Infisical:ClientId"] = "machine-client-id",
            ["Infisical:ClientSecret"] = "machine-client-secret",
            ["Infisical:SecretPath"] = "/backend",
            ["Infisical:Url"] = "https://example.infisical.com"
        };

        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(data)
            .Build();

        var config = MachineIdentityInfisicalConfig.FromConfiguration(configuration.GetSection("Infisical"));

        Assert.Equal("dev", config.Environment);
        Assert.Equal("workspace-id", config.ProjectId);
        Assert.Equal("machine-client-id", config.ClientId);
        Assert.Equal("machine-client-secret", config.ClientSecret);
        Assert.Equal("/backend", config.SecretPath);
        Assert.Equal("https://example.infisical.com", config.Url);
    }
}
