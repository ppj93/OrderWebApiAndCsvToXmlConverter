using System;
using System.Xml.Linq;
using Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;

namespace Mapper.Test
{
    [TestClass]
    public class XmlMapperTest
    {
        [TestMethod]
        public void GetOrderFromXmlReturnsErrorIfPassedNullInput()
        {
            var xmlMapper = new XmlMapper.XmlMapper();
            var mapperResult = xmlMapper.GetOrderFromXml(null, out Order order);
            Assert.AreEqual(OperationStatus.Fail, mapperResult.OperationStatus);
            Assert.AreEqual(mapperResult.Code, ResultCodes.InputValidationFail);
        }

        [TestMethod]
        public void GetOrderFromXmlReturnsErrorIfPassedInvalidOrderDate()
        {
            var xmlMapper = new XmlMapper.XmlMapper();
            var inputOrder = new XElement("Orders", new XElement(XmlNodeNames.Orders.Order), 
                new XElement(XmlNodeNames.Orders.Date, "NonParseableDate"));

            var mapperResult = xmlMapper.GetOrderFromXml(inputOrder, out Order order);
            Assert.AreEqual(OperationStatus.Fail, mapperResult.OperationStatus);
            Assert.AreEqual(mapperResult.Code, ResultCodes.ParseFail);
        }
    }
}
