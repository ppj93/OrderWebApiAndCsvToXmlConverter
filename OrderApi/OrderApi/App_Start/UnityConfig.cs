using OrderManager.Contract;
using System.Web.Http;
using Unity;
using Unity.WebApi;
using XmlMapper.Contracts;

namespace OrderApi
{
    public static class UnityConfig
    {
        private static readonly IUnityContainer Container = new UnityContainer();

        public static void RegisterComponents()
        {
            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            Container.RegisterType<IXmlMapper, XmlMapper.XmlMapper>();
            //Order Manager is a singleton - there is no state
            Container.RegisterInstance<IOrderManager>(new OrderManager.OrderManager(Container.Resolve<IXmlMapper>()));

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(Container);
        }

        //below method is to be only used in Main Project to have an object to kinc off the executoin
        //Every where else, dependencies will get automatically resolved as injected in constructors

        internal static T Resolve<T>()
        {
            return Container.Resolve<T>();
        }
    }
}