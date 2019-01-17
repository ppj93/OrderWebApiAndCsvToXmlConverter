using Common;
using Models;
using Models.Response;
using OrderManager.Contract;
using System.Web.Http;

namespace OrderApi.Controllers
{
    public class OrderController : ApiController
    {
        private readonly IOrderManager _orderManager;

        public OrderController(IOrderManager orderManager)
        {
            _orderManager = orderManager;
        }

        /*
          This project is designed assuming we have adequate Main Memory and we want to optimize on the Get Speed.
          Thus We store all the orders in Dictionary and on receiving a request, we just query the dictionary.
        */
        [HttpGet]
        public GetOrderResponse Get(long orderNumber)
        {
            if (orderNumber < 0)
                return new GetOrderResponse(Result.GetFailedResult(ResultCodes.InputValidationFail, "Order number is less than 0"));

            var searchOrderStatus = _orderManager.Get(orderNumber, out Order matchOrder);
            return new GetOrderResponse(searchOrderStatus, matchOrder);

        }

        [HttpGet]
        public string StartUp()
        {
            return "Thanks for visiting. We can run the Search Order API by giving a relative URL of /order/get?orderNumber=18935. Ex. http://localhost:64829/order/get?orderNumber=17642";
        }
    }
}
