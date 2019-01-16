using Common;
using OrderManager.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Configuration;
using System.Web.Http;
using System.IO;

namespace OrderApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        private IOrderManager _orderManager;

        //Parametrized constructor for ease of unit testing
        public WebApiApplication(IOrderManager orderManager)
        {
            _orderManager = orderManager;
        }

        public WebApiApplication()
        {
        }

        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            UnityConfig.RegisterComponents();

            var constructOrderDictionaryResultList = ConstructOrderSearchDictionary();
            if(!constructOrderDictionaryResultList.Any(result => result.OperationStatus == OperationStatus.Success))
                //Log the failure reason here to Log Files
                throw new Exception("Could not create OrderSearch Dictionary. Hence not starting Web API");
        }

        private List<Result> ConstructOrderSearchDictionary()
        {
            var xmlPath = WebConfigurationManager.AppSettings["OrderXmlFilePath"];
            _orderManager = _orderManager ?? UnityConfig.Resolve<IOrderManager>();
            var resultList = _orderManager.ConstructOrderSearchDictionary(xmlPath);

            var failedResults = resultList.Where(result => result.OperationStatus == OperationStatus.Fail).ToList();

            if (failedResults.Any())
            {
                var resultPath = WebConfigurationManager.AppSettings["OrderSearchDictionaryConstructionResult"];
                foreach (var result in failedResults)
                    File.WriteAllText(resultPath, $"{result.OperationStatus}, {result.Code}, {result.Message}");
            }

            return resultList;
        }
    }
}
