using OrderCsvToXmlConverter.Contracts;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
