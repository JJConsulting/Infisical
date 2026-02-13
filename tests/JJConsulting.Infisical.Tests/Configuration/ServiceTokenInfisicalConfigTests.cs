using JJConsulting.Infisical.Configuration;

namespace JJConsulting.Infisical.Tests.Configuration;

public class ServiceTokenInfisicalConfigTests
{
    [Fact]
    public void IsValid_ReturnsTrue_WhenServiceTokenPresent()
    {
        var config = new ServiceTokenInfisicalConfig
        {
            Environment = "prod",
            ProjectId = "project-id",
            ServiceToken = "service-token"
        };

        Assert.True(config.IsValid());
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void IsValid_ReturnsFalse_WhenServiceTokenMissing(string? serviceToken)
    {
        var config = new ServiceTokenInfisicalConfig
        {
            Environment = "prod",
            ProjectId = "project-id",
            ServiceToken = serviceToken!
        };

        Assert.False(config.IsValid());
    }

    [Fact]
    public async Task CreateAuthenticationService_ReturnsConfiguredServiceToken()
    {
        var config = new ServiceTokenInfisicalConfig
        {
            Environment = "prod",
            ProjectId = "project-id",
            ServiceToken = "service-token"
        };

        var authenticationService = config.CreateAuthenticationService();

        var token = await authenticationService.GetBearerTokenAsync();

        Assert.Equal("service-token", token);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public async Task CreateAuthenticationService_Throws_WhenServiceTokenMissing(string? serviceToken)
    {
        var config = new ServiceTokenInfisicalConfig
        {
            Environment = "prod",
            ProjectId = "project-id",
            ServiceToken = serviceToken!
        };

        var authenticationService = config.CreateAuthenticationService();

        var exception = await Assert.ThrowsAsync<InvalidOperationException>(() =>
            authenticationService.GetBearerTokenAsync());

        Assert.Equal(
            "Infisical ServiceToken must be set when using ServiceTokenInfisicalConfig.",
            exception.Message);
    }
}
