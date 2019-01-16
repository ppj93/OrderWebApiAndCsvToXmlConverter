using Models;
using System.Web.Http;

namespace OrderApi.Controllers
{
    public class OrderController : ApiController
    {
        [HttpGet]
        public Order xyz() {
            return new Order() { CustomerDetails = new Customer() { Name = "hello", Number = 122} };
        }
    }
}
