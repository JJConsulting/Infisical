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

}
