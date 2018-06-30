using System.Configuration;
using prmToolkit.Validation;


namespace Fbtc.Infra.Helpers
{
    public static class ConfigHelper
    {
        public static string GetKeyAppSetting(string key)
        {
            var value = ConfigurationManager.AppSettings[key];

            RaiseException.IfNullOrEmpty(value, $"Chave {key} Não definida no AppSettings. Verifique seu WebConfig ou AppSettings", true);

            return value;
        }

        public static string GetConnectionString(string key)
        {
            var connectionString = ConfigurationManager.ConnectionStrings[key]?.ConnectionString;

            RaiseException.IfNullOrEmpty(connectionString, $"Chave {key} Não definida no ConnectionStrings. Verifique seu WebConfig ou AppSettings", true);

            return connectionString;

        }
    }
}
