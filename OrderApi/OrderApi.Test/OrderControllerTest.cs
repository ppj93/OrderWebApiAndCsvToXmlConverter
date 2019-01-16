using System;
using System.Collections.Generic;
using Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using NSubstitute;
using OrderApi.Controllers;
using OrderManager.Contract;

namespace OrderApi.Test
{
    [TestClass]
    public class OrderControllerTest
    {
        private IOrderManager _mockOrderManager;

        [TestInitialize]
        public void BeforeEachTest()
        {
            _mockOrderManager = NSubstitute.Substitute.For<IOrderManager>();
        }

        [TestMethod]
        public void GetReturnsFailIfOrderNumberLessThanZero()
        {
            var orderCtrl = new OrderController(_mockOrderManager);
            var getOrderResponse = orderCtrl.Get(-122);
            Assert.AreEqual(OperationStatus.Fail, getOrderResponse.Result.OperationStatus);
            Assert.AreEqual(getOrderResponse.Result.Code, ResultCodes.InputValidationFail);
        }

        [TestMethod]
        public void GetReturnsOrderAsReturnedByOrderManagerForOrderNumberGreaterThanZero()
        {
            var orderCtrl = new OrderController(_mockOrderManager);

            var matchedOrder = new Order() { Number = 122, Products = new List<OrderProduct>() };
            _mockOrderManager.Get(122, out Order order).ReturnsForAnyArgs(args =>
            {
                args[1] = matchedOrder;
                return Result.GetSuccessResult();
            });
            var getOrderResponse = orderCtrl.Get(122);

            Assert.AreEqual(OperationStatus.Success, getOrderResponse.Result.OperationStatus);
            Assert.AreEqual(matchedOrder, getOrderResponse.Order);
        }
    }
}
