namespace Microsoft.Extensions.Configuration
{
    public static class IConfigurationRootExtensions
    {
        public static TOptions Bind<TOptions>(this IConfigurationRoot configurationRoot, string sectionName)
            where TOptions : new()
        {
            var options = new TOptions();

            configurationRoot.GetSection(sectionName).Bind(options);

            return options;
        }
    }
}
