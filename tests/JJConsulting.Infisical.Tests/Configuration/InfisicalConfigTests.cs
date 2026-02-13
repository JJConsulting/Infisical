using JJConsulting.Infisical.Configuration;

namespace JJConsulting.Infisical.Tests.Configuration;

public class InfisicalConfigTests
{
    [Fact]
    public void Url_StripsApiSuffix_WhenProvided()
    {
        var config = new ServiceTokenInfisicalConfig
        {
            Environment = "prod",
            ProjectId = "project-id",
            ServiceToken = "token",
            Url = "https://example.infisical.com/api"
        };

        Assert.Equal("https://example.infisical.com", config.Url);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void Url_Throws_WhenMissing(string? invalidUrl)
    {
        var exception = Assert.Throws<InvalidOperationException>(() =>
        {
            _ = new ServiceTokenInfisicalConfig
            {
                Environment = "prod",
                ProjectId = "project-id",
                ServiceToken = "token",
                Url = invalidUrl!
            };
        });

        Assert.Equal("InfisicalUrl must be set", exception.Message);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void SecretPath_Throws_WhenMissing(string? invalidSecretPath)
    {
        var exception = Assert.Throws<InvalidOperationException>(() =>
        {
            _ = new ServiceTokenInfisicalConfig
            {
                Environment = "prod",
                ProjectId = "project-id",
                ServiceToken = "token",
                SecretPath = invalidSecretPath!
            };
        });

        Assert.Equal("SecretPath must be set", exception.Message);
    }
}
