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

## Current Public API

Configuration types:

- `InfisicalConfig`
- `MachineIdentityInfisicalConfig`
- `ServiceTokenInfisicalConfig`

Extension methods:

- `IHostBuilder.AddInfisical(ServiceTokenInfisicalConfig config)`
- `IHostBuilder.AddInfisical(MachineIdentityInfisicalConfig config)`
- `IHostBuilder.AddInfisical<TAuthenticationService>(InfisicalConfig config)`
- `IConfigurationBuilder.AddInfisical(ServiceTokenInfisicalConfig config)`
- `IConfigurationBuilder.AddInfisical(MachineIdentityInfisicalConfig config)`
- `IConfigurationBuilder.AddInfisical<TAuthenticationService>(InfisicalConfig config)`
- `IServiceCollection.AddInfisical(ServiceTokenInfisicalConfig config)`
- `IServiceCollection.AddInfisical(MachineIdentityInfisicalConfig config)`
- `IServiceCollection.AddInfisical<TAuthenticationService>(InfisicalConfig config)`

Services:

- `IInfisicalAuthenticationService`
  - `Task<string> GetBearerTokenAsync()`
- `IInfisicalSecretsService`
  - `Task<InfisicalSecretsList> GetSecretsAsync()`
  - `Task<InfisicalSecret> GetSecretAsync(string key)`

Models:

- `InfisicalSecret`
- `InfisicalSecretsList`
- `InfisicalSecretListItem`

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
