using OrderManager.Contract;
using Unity;

namespace OrderApi
{
    public static class UnityConfig
    {
        private static readonly IUnityContainer _container = new UnityContainer();

        //More methods like RegisterServices, RegiterDataProviders can added in this class.
        internal static void RegisterTypes()
        {
            _container.RegisterType<IOrderManager, OrderManager.OrderManager>();
        }

        //below method is to be only used in Main Project to have an object to kinc off the executoin
        //Every where else, dependencies will get automatically resolved as injected in constructors

        internal static T Resolve<T>()
        {
            return _container.Resolve<T>();
        }



    }
}