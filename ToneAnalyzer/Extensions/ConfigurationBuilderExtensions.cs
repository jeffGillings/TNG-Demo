namespace Microsoft.Extensions.Configuration
{
    using System.IO;

    public static class ConfigurationBuilderExtensions
    {
        public static IConfigurationRoot BuildCustomisedConfiguration(this ConfigurationBuilder builder) =>
            builder
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();
    }
}
