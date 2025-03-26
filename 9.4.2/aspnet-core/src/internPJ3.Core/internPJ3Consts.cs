using internPJ3.Debugging;

namespace internPJ3
{
    public class internPJ3Consts
    {
        public const string LocalizationSourceName = "internPJ3";

        public const string ConnectionStringName = "Default";

        public const bool MultiTenancyEnabled = true;


        /// <summary>
        /// Default pass phrase for SimpleStringCipher decrypt/encrypt operations
        /// </summary>
        public static readonly string DefaultPassPhrase =
            DebugHelper.IsDebug ? "gsKxGZ012HLL3MI5" : "7c1e4440bec54183b8e70f8d3c3fa5ed";
    }
}
