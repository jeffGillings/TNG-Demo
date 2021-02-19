namespace Microsoft.Extensions.Configuration
{
    public static class IConfigurationExtensions
    {
        public static TOptions Bind<TOptions>(this IConfiguration configuration, string sectionName)
            where TOptions : new()
        {
            var options = new TOptions();

            configuration.GetSection(sectionName).Bind(options);

            return options;
        }
    }
}
