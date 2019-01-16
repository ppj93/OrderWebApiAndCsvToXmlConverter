using System.Configuration;

namespace OrderCsvToXmlConverter.Contracts
{
    interface IUtilities
    {
        T GetAppSetting<T>(string key, AppSettingsReader appSettingsReader = null); //Nullable parameter for unit testing
    }
}
