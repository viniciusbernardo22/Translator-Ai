using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Translator_Ai.Infraestructure.Services;

namespace Translator_Ai.Infraestructure.Configurations.Translator
{
    public static class TranslatorConfigurations
    {
        public static IServiceCollection AddAzureTranslator(this IServiceCollection services, IConfiguration configuration)
        {
            IConfigurationSection? section = configuration.GetSection("App:AzureTranslator");

            ValidateSection(section);

            services.Configure<TranslatorOptions>(section);
            services.AddHttpClient<ITranslatorService, TranslatorService>();
            return services;
        }

        private static void ValidateSection(IConfigurationSection section)
        {
            var endpoint = section.GetSection("Endpoint")!.Value;
            var key = section.GetSection("Key")!.Value;

            if (string.IsNullOrEmpty(endpoint) || string.IsNullOrEmpty(key))
                throw new ArgumentException("Translator not correctly configured, please check your appsettings/dotnet secrets");
        }
    }
}
