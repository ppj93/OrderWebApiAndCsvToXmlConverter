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

        [HttpGet]
        public GetOrderResponse Get(long orderNumber)
        {
            if (orderNumber < 0)
                return new GetOrderResponse(Result.GetFailedResult(ResultCodes.InputValidationFail, "Order number is less than 0"));

            var searchOrderStatus = _orderManager.Get(orderNumber, out Order matchOrder);
            return new GetOrderResponse(searchOrderStatus, matchOrder);

        }
    }
}
