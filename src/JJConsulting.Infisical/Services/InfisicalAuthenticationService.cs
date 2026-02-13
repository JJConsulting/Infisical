using System.Net.Http.Json;
using System.Text.Json;
using JJConsulting.Infisical.Configuration;
using JJConsulting.Infisical.Models;
using JJConsulting.Infisical.Serialization;

namespace JJConsulting.Infisical.Services;

/// <summary>
/// Provides authentication tokens used when calling the Infisical API.
/// </summary>
public interface IInfisicalAuthenticationService
{
    /// <summary>
    /// Gets a bearer token for Infisical API requests.
    /// </summary>
    /// <returns>A bearer token string.</returns>
    Task<string> GetBearerTokenAsync();
}

internal sealed class MachineIdentityAuthenticationService(MachineIdentityInfisicalConfig config) : IInfisicalAuthenticationService
{
    public async Task<string> GetBearerTokenAsync()
    {
        var body = new
        {
            clientId = config.ClientId,
            clientSecret = config.ClientSecret
        };

        using var httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri(config.Url);

        using var response = await httpClient.PostAsJsonAsync(
            $"{config.Url}/api/v1/auth/universal-auth/login",
            body
        );

        response.EnsureSuccessStatusCode();

        await using var stream = await response.Content.ReadAsStreamAsync();
        var login = JsonSerializer.Deserialize<InfisicalMachineIdentityLogin>(stream, InfisicalJsonOptions.Value);

        return login!.AccessToken;
    }
}

internal sealed class ServiceTokenAuthenticationService(ServiceTokenInfisicalConfig config) : IInfisicalAuthenticationService
{
    public Task<string> GetBearerTokenAsync()
    {
        var serviceToken = config.ServiceToken;

        if (string.IsNullOrWhiteSpace(serviceToken))
        {
            throw new InvalidOperationException(
                "Infisical ServiceToken must be set when using ServiceTokenInfisicalConfig.");
        }

        return Task.FromResult(serviceToken);
    }
}
