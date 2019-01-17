using System;
using System.Linq;
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

        [TestMethod]
        public void GetOrderFromXmlReturnsErrorIfPassedInvalidOrderNumber()
        {
            var xmlMapper = new XmlMapper.XmlMapper();
            var inputOrder = new XElement("Orders", new XElement(XmlNodeNames.Orders.Order),
                new XElement(XmlNodeNames.Orders.Number, "NonParseable")
                );

            var mapperResult = xmlMapper.GetOrderFromXml(inputOrder, out Order order);
            Assert.AreEqual(OperationStatus.Fail, mapperResult.OperationStatus);
            Assert.AreEqual(mapperResult.Code, ResultCodes.ParseFail);
        }

        [TestMethod]
        public void GetOrderFromXmlParsesOrderInformationCorrectly()
        {
            var xmlMapper = new XmlMapper.XmlMapper();
            var inputOrder = new XElement("Orders", new XElement(XmlNodeNames.Orders.Order),
                new XElement(XmlNodeNames.Orders.Date, "2019-01-01"),
                new XElement(XmlNodeNames.Orders.Number, "12")
                );

            var mapperResult = xmlMapper.GetOrderFromXml(inputOrder, out Order order);
            Assert.AreEqual(OperationStatus.Success, mapperResult.OperationStatus);
            Assert.AreEqual(12, order.Number);
            Assert.AreEqual(DateTime.Parse("2019-01-01"), order.Date.Value);
        }

        [TestMethod]
        public void GetOrderFromXmlParsesProductInformationCorrectly()
        {
            var xmlMapper = new XmlMapper.XmlMapper();

            var productElement = new XElement(XmlNodeNames.Orders.Products, new XElement(XmlNodeNames.Product.Title,
                new XElement(XmlNodeNames.Product.Number, "122"),
                new XElement(XmlNodeNames.Product.Description, "Description1"),
                new XElement(XmlNodeNames.Product.Price, "9.12"),
                new XElement(XmlNodeNames.Product.Quantity, "12"),
                new XElement(XmlNodeNames.Product.Name, "XYZ Computer"),
                new XElement(XmlNodeNames.Product.Group, "Group1")));

            var customerElement = new XElement(XmlNodeNames.Customer.Title,
              new XElement(XmlNodeNames.Customer.Number, "122"),
              new XElement(XmlNodeNames.Customer.Name, "Pravin"));

            var inputOrder = new XElement("Orders", new XElement(XmlNodeNames.Orders.Order),
                new XElement(XmlNodeNames.Orders.Date, "2019-01-01"),
                new XElement(XmlNodeNames.Orders.Number, "12"),
                productElement,
                customerElement
                );

            var mapperResult = xmlMapper.GetOrderFromXml(inputOrder, out Order order);
            Assert.AreEqual(OperationStatus.Success, mapperResult.OperationStatus);
            Assert.AreEqual(order.Products.Count, 1);
            var product = order.Products.First();
            Assert.AreEqual("Group1", product.Group);
            Assert.AreEqual((decimal)9.12, product.Price);
            Assert.AreEqual(12, product.Quantity);
            Assert.AreEqual("Description1", product.Description);
            Assert.AreEqual("122" , product.Number);
            Assert.AreEqual("XYZ Computer", product.Name);
        }

        [TestMethod]
        public void GetOrderFromXmlParsesCustomerInformationCorrectly()
        {
            var xmlMapper = new XmlMapper.XmlMapper();

            var customerElement = new XElement(XmlNodeNames.Customer.Title,
                new XElement(XmlNodeNames.Customer.Number, "122"),
                new XElement(XmlNodeNames.Customer.Name, "Pravin"));
            var productElement = new XElement(XmlNodeNames.Orders.Products, new XElement(XmlNodeNames.Product.Title,
              new XElement(XmlNodeNames.Product.Number, "122"),
              new XElement(XmlNodeNames.Product.Description, "Description1"),
              new XElement(XmlNodeNames.Product.Price, "9.12"),
              new XElement(XmlNodeNames.Product.Quantity, "12"),
              new XElement(XmlNodeNames.Product.Name, "XYZ Computer"),
              new XElement(XmlNodeNames.Product.Group, "Group1")));

            var inputOrder = new XElement("Orders", new XElement(XmlNodeNames.Orders.Order),
                new XElement(XmlNodeNames.Orders.Date, "2019-01-01"),
                new XElement(XmlNodeNames.Orders.Number, "12"),
                customerElement,
                productElement
                );

            var mapperResult = xmlMapper.GetOrderFromXml(inputOrder, out Order order);
            Assert.AreEqual(OperationStatus.Success, mapperResult.OperationStatus);
            Assert.AreEqual(122, order.CustomerDetails.Number);
            Assert.AreEqual("Pravin", order.CustomerDetails.Name);
        }

        [TestMethod]
        public void GetOrderFromXmlReturnsParseErrorIfProductPriceIsNotParseable()
        {
            var xmlMapper = new XmlMapper.XmlMapper();

            var customerElement = new XElement(XmlNodeNames.Customer.Title,
                new XElement(XmlNodeNames.Customer.Number, "122"),
                new XElement(XmlNodeNames.Customer.Name, "Pravin"));
            var productElement = new XElement(XmlNodeNames.Orders.Products, new XElement(XmlNodeNames.Product.Title,
              new XElement(XmlNodeNames.Product.Number, "122"),
              new XElement(XmlNodeNames.Product.Description, "Description1"),
              new XElement(XmlNodeNames.Product.Price, "NonParseable"),
              new XElement(XmlNodeNames.Product.Quantity, "12"),
              new XElement(XmlNodeNames.Product.Name, "XYZ Computer"),
              new XElement(XmlNodeNames.Product.Group, "Group1")));

            var inputOrder = new XElement("Orders", new XElement(XmlNodeNames.Orders.Order),
                new XElement(XmlNodeNames.Orders.Date, "2019-01-01"),
                new XElement(XmlNodeNames.Orders.Number, "12"),
                customerElement,
                productElement
                );

            var mapperResult = xmlMapper.GetOrderFromXml(inputOrder, out Order order);
            Assert.AreEqual(OperationStatus.Fail, mapperResult.OperationStatus);
            Assert.AreEqual(mapperResult.Code, ResultCodes.ParseFail);
        }

        [TestMethod]
        public void GetOrderFromXmlReturnsParseErrorIfProductQuantityIsNotParseable()
        {
            var xmlMapper = new XmlMapper.XmlMapper();

            var customerElement = new XElement(XmlNodeNames.Customer.Title,
                new XElement(XmlNodeNames.Customer.Number, "122"),
                new XElement(XmlNodeNames.Customer.Name, "Pravin"));
            var productElement = new XElement(XmlNodeNames.Orders.Products, new XElement(XmlNodeNames.Product.Title,
              new XElement(XmlNodeNames.Product.Number, "122"),
              new XElement(XmlNodeNames.Product.Description, "Description1"),
              new XElement(XmlNodeNames.Product.Price, "99.67"),
              new XElement(XmlNodeNames.Product.Quantity, "NonParseable"),
              new XElement(XmlNodeNames.Product.Name, "XYZ Computer"),
              new XElement(XmlNodeNames.Product.Group, "Group1")));

            var inputOrder = new XElement("Orders", new XElement(XmlNodeNames.Orders.Order),
                new XElement(XmlNodeNames.Orders.Date, "2019-01-01"),
                new XElement(XmlNodeNames.Orders.Number, "12"),
                customerElement,
                productElement
                );

            var mapperResult = xmlMapper.GetOrderFromXml(inputOrder, out Order order);
            Assert.AreEqual(OperationStatus.Fail, mapperResult.OperationStatus);
            Assert.AreEqual(mapperResult.Code, ResultCodes.ParseFail);
        }
        [TestMethod]
        public void GetOrderFromXmlReturnsParseErrorIfCustomerNumberIsNotParseable()
        {
            var xmlMapper = new XmlMapper.XmlMapper();

            var customerElement = new XElement(XmlNodeNames.Customer.Title,
                new XElement(XmlNodeNames.Customer.Number, "NonParseable"),
                new XElement(XmlNodeNames.Customer.Name, "Pravin"));
            var productElement = new XElement(XmlNodeNames.Orders.Products, new XElement(XmlNodeNames.Product.Title,
              new XElement(XmlNodeNames.Product.Number, "122"),
              new XElement(XmlNodeNames.Product.Description, "Description1"),
              new XElement(XmlNodeNames.Product.Price, "99.67"),
              new XElement(XmlNodeNames.Product.Quantity, "45"),
              new XElement(XmlNodeNames.Product.Name, "XYZ Computer"),
              new XElement(XmlNodeNames.Product.Group, "Group1")));

            var inputOrder = new XElement("Orders", new XElement(XmlNodeNames.Orders.Order),
                new XElement(XmlNodeNames.Orders.Date, "2019-01-01"),
                new XElement(XmlNodeNames.Orders.Number, "12"),
                customerElement,
                productElement
                );

            var mapperResult = xmlMapper.GetOrderFromXml(inputOrder, out Order order);
            Assert.AreEqual(OperationStatus.Fail, mapperResult.OperationStatus);
            Assert.AreEqual(mapperResult.Code, ResultCodes.ParseFail);
        }


    }
}
