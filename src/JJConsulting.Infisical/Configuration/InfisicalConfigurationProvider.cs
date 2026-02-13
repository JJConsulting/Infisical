using JJConsulting.Infisical.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace JJConsulting.Infisical.Configuration;

internal sealed class InfisicalConfigurationProvider : ConfigurationProvider
{
    private readonly Dictionary<string, string> _secretsCache = new();
    private readonly InfisicalSecretsService _secretsService;

    public InfisicalConfigurationProvider(InfisicalConfig config)
    {
        var options = Options.Create(config);
        var authenticationService = config.CreateAuthenticationService();
        
        _secretsService = new InfisicalSecretsService(authenticationService, options);
    }

    public override void Load()
    {
        var task = LoadAsync();
        task.GetAwaiter().GetResult();

        if (task.Exception is not null)
        {
            throw task.Exception.InnerException ?? task.Exception;
        }
    }

    private async Task LoadAsync()
    {
        try
        {
            var secrets = await _secretsService.GetSecretsAsync();

            _secretsCache.Clear();
            Data.Clear();

            foreach (var secret in secrets.Secrets)
            {
                var key = secret.Key.Replace("__", ":");
                Data[key] = secret.Value;
                _secretsCache[key] = secret.Value;
            }
        }
        catch
        {
            Data.Clear();
            foreach (var kv in _secretsCache)
            {
                Data[kv.Key] = kv.Value;
            }

            throw;
        }
    }
}
