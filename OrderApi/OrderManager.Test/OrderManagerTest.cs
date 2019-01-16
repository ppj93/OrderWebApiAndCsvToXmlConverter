using System;
using System.Collections.Generic;
using Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;

namespace OrderManager.Test
{
    [TestClass]
    public class OrderManagerTest
    {
        [TestMethod]
        public void GetCorrectlySearchesAndReturnsOrder()
        {
            var orderIdToOrderDictionary = new Dictionary<long, Order>();
            var order = new Order() { Number = 12 };
            orderIdToOrderDictionary.Add(12, order);
            var orderManager = new OrderManager(null, orderIdToOrderDictionary);
            var getOrderStatus = orderManager.Get(12, out Order matchedOrder);
            Assert.AreEqual(12, matchedOrder.Number);
            Assert.AreEqual(OperationStatus.Success, getOrderStatus.OperationStatus);
        }

        [TestMethod]
        public void GetReturnsFailStatusIfOrderNumberIsNotFoundInTheDictionary()
        {
            var orderIdToOrderDictionary = new Dictionary<long, Order>();
            var order = new Order() { Number = 12 };
            orderIdToOrderDictionary.Add(12, order);
            var orderManager = new OrderManager(null, orderIdToOrderDictionary);
            var getOrderStatus = orderManager.Get(44, out Order matchedOrder);
            Assert.IsNull(matchedOrder);
            Assert.AreEqual(OperationStatus.Fail, getOrderStatus.OperationStatus);
            Assert.AreEqual(getOrderStatus.Code, ResultCodes.OrderMatchingCriteriaNotFound);
        }
    }
}
