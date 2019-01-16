using OrderCsvToXmlConverter.Contracts;
using System.Configuration;

namespace OrderCsvToXmlConverter
{
    class Utilities:IUtilities
    {
        public T GetAppSetting<T>(string key, AppSettingsReader appSettingsReader=null) //Nullable parameter for unit testing
        {
            appSettingsReader = appSettingsReader ?? new AppSettingsReader();
            return (T)appSettingsReader.GetValue(key, typeof(T));
        }
    }
}
