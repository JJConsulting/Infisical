using JJConsulting.Infisical.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace JJConsulting.Infisical.Configuration;

public static class InfisicalConfigurationExtensions
{
    extension(IHostBuilder builder)
    {
        public IHostBuilder AddInfisical(ServiceTokenInfisicalConfig config)
        {
            builder.ConfigureAppConfiguration(configurationBuilder =>
            {
                configurationBuilder.AddInfisical(config);
            });

            builder.ConfigureServices(services => services.AddInfisical(config));

            return builder;
        }

        public IHostBuilder AddInfisical(MachineIdentityInfisicalConfig config)
        {
            builder.ConfigureAppConfiguration(configurationBuilder =>
            {
                configurationBuilder.AddInfisical(config);
            });

            builder.ConfigureServices(services => services.AddInfisical(config));

            return builder;
        }

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
        public IConfigurationBuilder AddInfisical(ServiceTokenInfisicalConfig config)
        {
            return builder.Add(new InfisicalConfigurationSource(
                config,
                () => new ServiceTokenAuthenticationService(config)));
        }

        public IConfigurationBuilder AddInfisical(MachineIdentityInfisicalConfig config)
        {
            return builder.Add(new InfisicalConfigurationSource(
                config,
                () => new MachineIdentityAuthenticationService(config)));
        }

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
        public IServiceCollection AddInfisical(ServiceTokenInfisicalConfig config)
        {
            services.AddSingleton<InfisicalConfig>(config);
            services.AddSingleton(config);
            services.AddSingleton<IOptions<InfisicalConfig>>(Options.Create(config));
            services.AddTransient<IInfisicalAuthenticationService, ServiceTokenAuthenticationService>();
            services.AddTransient<IInfisicalSecretsService, InfisicalSecretsService>();

            return services;
        }

        public IServiceCollection AddInfisical(MachineIdentityInfisicalConfig config)
        {
            services.AddSingleton<InfisicalConfig>(config);
            services.AddSingleton(config);
            services.AddSingleton<IOptions<InfisicalConfig>>(Options.Create(config));
            services.AddTransient<IInfisicalAuthenticationService, MachineIdentityAuthenticationService>();
            services.AddTransient<IInfisicalSecretsService, InfisicalSecretsService>();

            return services;
        }

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
