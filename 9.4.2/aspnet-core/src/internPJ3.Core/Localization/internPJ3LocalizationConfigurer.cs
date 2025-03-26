using Abp.Configuration.Startup;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Reflection.Extensions;

namespace internPJ3.Localization
{
    public static class internPJ3LocalizationConfigurer
    {
        public static void Configure(ILocalizationConfiguration localizationConfiguration)
        {
            localizationConfiguration.Sources.Add(
                new DictionaryBasedLocalizationSource(internPJ3Consts.LocalizationSourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        typeof(internPJ3LocalizationConfigurer).GetAssembly(),
                        "internPJ3.Localization.SourceFiles"
                    )
                )
            );
        }
    }
}
