using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderCsvToXmlConverter.Contracts
{
    interface IUtilities
    {
        T GetAppSetting<T>(string key, AppSettingsReader appSettingsReader = null); //Nullable parameter for unit testing
    }
}
