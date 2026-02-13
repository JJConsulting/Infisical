# JJConsulting.Infisical

`JJConsulting.Infisical` integrates Infisical secrets with .NET configuration and dependency injection.

## Installation

Install from NuGet:

```bash
dotnet add package JJConsulting.Infisical
```

Or add a package reference manually:

```xml
<ItemGroup>
  <PackageReference Include="JJConsulting.Infisical" Version="1.0.0" />
</ItemGroup>
```

Or with Package Manager Console:

```powershell
Install-Package JJConsulting.Infisical
```

## Public API At A Glance

### Configuration types

- `ServiceTokenInfisicalConfig`: use Infisical service token authentication.
- `MachineIdentityInfisicalConfig`: use machine identity (`ClientId`/`ClientSecret`) authentication.
- `InfisicalConfig`: base configuration type (inherit it to use with custom authentication services).

### Extension methods

Use whichever startup surface your app already uses:

`IHostBuilder` - Configures both the Infisical configuration provider and DI services

- `AddInfisical(ServiceTokenInfisicalConfig config)`
- `AddInfisical(MachineIdentityInfisicalConfig config)`
- `AddInfisical<TAuthenticationService>(InfisicalConfig config)`

`IConfigurationBuilder` - Registers only the Infisical configuration provider

- `AddInfisical(ServiceTokenInfisicalConfig config)`
- `AddInfisical(MachineIdentityInfisicalConfig config)`
- `AddInfisical<TAuthenticationService>(InfisicalConfig config)`

`IServiceCollection` - Registers only the Infisical DI services

- `AddInfisical(ServiceTokenInfisicalConfig config)`
- `AddInfisical(MachineIdentityInfisicalConfig config)`
- `AddInfisical<TAuthenticationService>(InfisicalConfig config)`

### Registered services

- `IInfisicalAuthenticationService`
- `GetBearerTokenAsync()`: returns the bearer token used for Infisical API calls.
- `IInfisicalSecretsService`
- `GetSecretsAsync()`: fetches all secrets for the configured project/path/environment.
- `GetSecretAsync(string key)`: fetches one secret by key.

## Configure With Service Token

`appsettings.json`:

```json
{
  "Infisical": {
    "Environment": "prod",
    "ProjectId": "your-project-id",
    "ServiceToken": "your-service-token",
    "SecretPath": "/",
    "Url": "https://app.infisical.com"
  }
}
```

`Program.cs`:

```csharp
using JJConsulting.Infisical.Configuration;

var infisicalConfig = ServiceTokenInfisicalConfig
    .FromConfiguration(builder.Configuration.GetSection("Infisical"));

builder.Host.AddInfisical(infisicalConfig);
```

## Configure With Machine Identity

`appsettings.json`:

```json
{
  "Infisical": {
    "Environment": "prod",
    "ProjectId": "your-project-id",
    "ClientId": "your-client-id",
    "ClientSecret": "your-client-secret",
    "SecretPath": "/",
    "Url": "https://app.infisical.com"
  }
}
```

`Program.cs`:

```csharp
using JJConsulting.Infisical.Configuration;

var infisicalConfig = MachineIdentityInfisicalConfig
    .FromConfiguration(builder.Configuration.GetSection("Infisical"));

builder.Host.AddInfisical(infisicalConfig);
```

## Secret Naming For .NET Configuration

When creating secrets in Infisical for `.NET`, use double underscore (`__`) in the secret key to represent nesting.

Examples of keys in Infisical:

- `MySettings__MyKey`
- `ConnectionStrings__DefaultConnection`
- `Logging__LogLevel__Default`

In `.NET`, these are available as standard configuration paths with `:`.

Example with `IConfiguration`:

```csharp
var value = builder.Configuration["MySettings:MyKey"];
var defaultConnection = builder.Configuration.GetConnectionString("DefaultConnection");
var logLevel = builder.Configuration["Logging:LogLevel:Default"];
```

## Direct Secret Access

`IInfisicalSecretsService` is registered by `AddInfisical`:

```csharp
using JJConsulting.Infisical.Services;

public class MyService(IInfisicalSecretsService secretsService)
{
    public async Task UseSecretsAsync()
    {
        var all = await secretsService.GetSecretsAsync();
        var single = await secretsService.GetSecretAsync("DATABASE_PASSWORD");
    }
}
```

## Notes

- `Url` defaults to `https://app.infisical.com`.
- If `Url` ends with `/api`, it is normalized automatically.
- `SecretPath` defaults to `/` and cannot be empty.
- Service-token auth throws if `ServiceToken` is null/empty/whitespace.

## License

MIT
