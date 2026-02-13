using System.Net.Http.Headers;
using System.Text.Json;
using JJConsulting.Infisical.Configuration;
using JJConsulting.Infisical.Models;
using JJConsulting.Infisical.Serialization;
using Microsoft.Extensions.Options;

namespace JJConsulting.Infisical.Services;

public interface IInfisicalSecretsService
{
    Task<InfisicalSecretsList> GetSecretsAsync();
    Task<InfisicalSecret> GetSecretAsync(string key);
}

internal sealed class InfisicalSecretsService(
    IInfisicalAuthenticationService authenticationService,
    IOptions<InfisicalConfig> options) : IInfisicalSecretsService
{
    private readonly InfisicalConfig _config = options.Value;

    public async Task<InfisicalSecretsList> GetSecretsAsync()
    {
        var url =
            $"/api/v3/secrets/raw/?environment={_config.Environment}&workspaceId={_config.ProjectId}&include_imports=true";

        var accessToken = await authenticationService.GetBearerTokenAsync();

        using var httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri(_config.Url);
        httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", accessToken);

        var response = await httpClient.GetAsync(url);
        var content = await response.Content.ReadAsStreamAsync();
        response.EnsureSuccessStatusCode();

        return JsonSerializer.Deserialize<InfisicalSecretsList>(content, InfisicalJsonOptions.Value)!;
    }

    public async Task<InfisicalSecret> GetSecretAsync(string key)
    {
        var url =
            $"/api/v3/secrets/raw/{Uri.EscapeDataString(key)}?environment={_config.Environment}&workspaceId={_config.ProjectId}&include_imports=true";

        var accessToken = await authenticationService.GetBearerTokenAsync();

        using var httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri(_config.Url);
        httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", accessToken);

        var response = await httpClient.GetAsync(url);
        var content = await response.Content.ReadAsStreamAsync();
        response.EnsureSuccessStatusCode();

        return JsonSerializer.Deserialize<InfisicalSecretEnvelope>(content, InfisicalJsonOptions.Value)!.Secret;
    }

}
