using JJConsulting.Infisical.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace JJConsulting.Infisical.Configuration;

/// <summary>
/// Extension methods to register Infisical configuration and services.
/// </summary>
public static class InfisicalConfigurationExtensions
{
    extension(IHostBuilder builder)
    {
        /// <summary>
        /// Adds Infisical to the host using service-token authentication.
        /// </summary>
        /// <param name="config">The Infisical configuration to use.</param>
        /// <returns>The same host builder for chaining.</returns>
        public IHostBuilder AddInfisical(ServiceTokenInfisicalConfig config)
        {
            builder.ConfigureAppConfiguration(configurationBuilder =>
            {
                configurationBuilder.AddInfisical(config);
            });

            builder.ConfigureServices(services => services.AddInfisical(config));

            return builder;
        }

        /// <summary>
        /// Adds Infisical to the host using machine-identity authentication.
        /// </summary>
        /// <param name="config">The Infisical configuration to use.</param>
        /// <returns>The same host builder for chaining.</returns>
        public IHostBuilder AddInfisical(MachineIdentityInfisicalConfig config)
        {
            builder.ConfigureAppConfiguration(configurationBuilder =>
            {
                configurationBuilder.AddInfisical(config);
            });

            builder.ConfigureServices(services => services.AddInfisical(config));

            return builder;
        }

        /// <summary>
        /// Adds Infisical to the host using a custom authentication service.
        /// </summary>
        /// <typeparam name="TAuthenticationService">
        /// The authentication service type used to obtain bearer tokens for Infisical API calls.
        /// </typeparam>
        /// <param name="config">The Infisical configuration to use.</param>
        /// <returns>The same host builder for chaining.</returns>
        public IHostBuilder AddInfisical<TAuthenticationService>(InfisicalConfig config)
            where TAuthenticationService : class, IInfisicalAuthenticationService
        {
            builder.ConfigureAppConfiguration(configurationBuilder =>
            {
                configurationBuilder.AddInfisical<TAuthenticationService>(config);
            });

            builder.ConfigureServices(services => services.AddInfisical<TAuthenticationService>(config));

            return builder;
        }
    }

    extension(IConfigurationBuilder builder)
    {
        /// <summary>
        /// Adds the Infisical configuration provider using service-token authentication.
        /// </summary>
        /// <param name="config">The Infisical configuration to use.</param>
        /// <returns>The same configuration builder for chaining.</returns>
        public IConfigurationBuilder AddInfisical(ServiceTokenInfisicalConfig config)
        {
            return builder.Add(new InfisicalConfigurationSource(
                config,
                () => new ServiceTokenAuthenticationService(config)));
        }

        /// <summary>
        /// Adds the Infisical configuration provider using machine-identity authentication.
        /// </summary>
        /// <param name="config">The Infisical configuration to use.</param>
        /// <returns>The same configuration builder for chaining.</returns>
        public IConfigurationBuilder AddInfisical(MachineIdentityInfisicalConfig config)
        {
            return builder.Add(new InfisicalConfigurationSource(
                config,
                () => new MachineIdentityAuthenticationService(config)));
        }

        /// <summary>
        /// Adds the Infisical configuration provider using a custom authentication service.
        /// </summary>
        /// <typeparam name="TAuthenticationService">
        /// The authentication service type used to obtain bearer tokens for Infisical API calls.
        /// </typeparam>
        /// <param name="config">The Infisical configuration to use.</param>
        /// <returns>The same configuration builder for chaining.</returns>
        public IConfigurationBuilder AddInfisical<TAuthenticationService>(InfisicalConfig config)
            where TAuthenticationService : class, IInfisicalAuthenticationService
        {
            return builder.Add(new InfisicalConfigurationSource(
                config,
                () => CreateAuthenticationService<TAuthenticationService>(config)));
        }
    }

    extension(IServiceCollection services)
    {
        /// <summary>
        /// Registers Infisical services using service-token authentication.
        /// </summary>
        /// <param name="config">The Infisical configuration to use.</param>
        /// <returns>The same service collection for chaining.</returns>
        public IServiceCollection AddInfisical(ServiceTokenInfisicalConfig config)
        {
            services.AddSingleton<InfisicalConfig>(config);
            services.AddSingleton(config);
            services.AddSingleton<IOptions<InfisicalConfig>>(Options.Create(config));
            services.AddTransient<IInfisicalAuthenticationService, ServiceTokenAuthenticationService>();
            services.AddTransient<IInfisicalSecretsService, InfisicalSecretsService>();

            return services;
        }

        /// <summary>
        /// Registers Infisical services using machine-identity authentication.
        /// </summary>
        /// <param name="config">The Infisical configuration to use.</param>
        /// <returns>The same service collection for chaining.</returns>
        public IServiceCollection AddInfisical(MachineIdentityInfisicalConfig config)
        {
            services.AddSingleton<InfisicalConfig>(config);
            services.AddSingleton(config);
            services.AddSingleton<IOptions<InfisicalConfig>>(Options.Create(config));
            services.AddTransient<IInfisicalAuthenticationService, MachineIdentityAuthenticationService>();
            services.AddTransient<IInfisicalSecretsService, InfisicalSecretsService>();

            return services;
        }

        /// <summary>
        /// Registers Infisical services using a custom authentication service.
        /// </summary>
        /// <typeparam name="TAuthenticationService">
        /// The authentication service type used to obtain bearer tokens for Infisical API calls.
        /// </typeparam>
        /// <param name="config">The Infisical configuration to use.</param>
        /// <returns>The same service collection for chaining.</returns>
        public IServiceCollection AddInfisical<TAuthenticationService>(InfisicalConfig config)
            where TAuthenticationService : class, IInfisicalAuthenticationService
        {
            services.AddSingleton<InfisicalConfig>(config);
            services.AddSingleton(config.GetType(), config);
            services.AddSingleton<IOptions<InfisicalConfig>>(Options.Create(config));

            services.AddTransient<IInfisicalSecretsService, InfisicalSecretsService>();
            services.AddTransient<IInfisicalAuthenticationService, TAuthenticationService>();

            return services;
        }
    }

    private static TAuthenticationService CreateAuthenticationService<TAuthenticationService>(InfisicalConfig config)
        where TAuthenticationService : class, IInfisicalAuthenticationService
    {
        var authenticationServiceType = typeof(TAuthenticationService);
        var configType = config.GetType();

        var matchingConstructor = authenticationServiceType
            .GetConstructors()
            .FirstOrDefault(constructor =>
            {
                var parameters = constructor.GetParameters();
                return parameters.Length == 1 &&
                       parameters[0].ParameterType.IsAssignableFrom(configType);
            });

        if (matchingConstructor is not null)
        {
            return (TAuthenticationService)matchingConstructor.Invoke([config]);
        }

        var parameterlessConstructor = authenticationServiceType.GetConstructor(Type.EmptyTypes);
        if (parameterlessConstructor is not null)
        {
            return (TAuthenticationService)parameterlessConstructor.Invoke(null);
        }

        throw new InvalidOperationException(
            $"{authenticationServiceType.FullName} must expose either a parameterless constructor or " +
            $"a single constructor argument compatible with {configType.FullName}.");
    }
}
