using OrderApi;
using Common;
using OrderManager.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Http;
using System.Web.Routing;

namespace OrderApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        private readonly IOrderManager _orderManager;

        //Parametrized constructor for ease of unit testing
        public WebApiApplication(IOrderManager orderManager=null)
        {
            if(orderManager == null)
            {
                UnityConfig.RegisterTypes();
                orderManager = UnityConfig.Resolve<IOrderManager>();
            }

            _orderManager = orderManager;
        }

        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            UnityConfig.RegisterTypes();

            var constructOrderDictionaryResult = ConstructOrderSearchDictionary();
            if(constructOrderDictionaryResult.OperationStatus != OperationStatus.Success)
                //Log the failure reason here to Log Files
                throw new Exception("Could not create OrderSearch Dictionary. Web API could not start");
        }

        private Result ConstructOrderSearchDictionary()
        {
            var xmlPath = WebConfigurationManager.AppSettings["OrderXmlFilePath"];
            return _orderManager.ConstructOrderSearchDictionary(xmlPath);
        }
    }
}
