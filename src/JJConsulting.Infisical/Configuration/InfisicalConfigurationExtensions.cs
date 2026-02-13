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
        public IHostBuilder AddInfisical(InfisicalConfig config)
        {
            builder.ConfigureAppConfiguration(configurationBuilder =>
            {
                configurationBuilder.AddInfisical(config);
            });
            
            builder.ConfigureServices(services => services.AddInfisical(config));

            return builder;
        }
    }
    
    extension(IConfigurationBuilder builder)
    {
        public IConfigurationBuilder AddInfisical(InfisicalConfig config)
        {
            return builder.Add(new InfisicalConfigurationSource(config));
        }
    }
    
    extension(IServiceCollection services)
    {
        public IServiceCollection AddInfisical(InfisicalConfig config)
        {
            services.AddSingleton(config);
            services.AddSingleton<IOptions<InfisicalConfig>>(Options.Create(config));

            services.AddTransient<IInfisicalSecretsService, InfisicalSecretsService>();
            services.AddTransient<IInfisicalAuthenticationService>(serviceProvider =>
            {
                var infisicalConfig = serviceProvider.GetRequiredService<InfisicalConfig>();
                return infisicalConfig.CreateAuthenticationService();
            });

            return services;
        }
    }
}
