﻿using Common;
using Models;
using OrderManager.Contract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using XmlMapper.Contracts;

namespace OrderManager
{
    public class OrderManager : IOrderManager
    {
        private readonly IXmlMapper _xmlMapper;
        public OrderManager(IXmlMapper xmlMapper)
        {
            _xmlMapper = xmlMapper;
        }
        private readonly IDictionary<long, Order> OrderIdToOrder;

        public OrderManager() : this(new Dictionary<long, Order>()) { }

        //Constructor for unit testing purposes
        public OrderManager(IDictionary<long, Order> orderIdToFileOffset)
        {
            OrderIdToOrder = orderIdToFileOffset;
        }

        public List<Result> ConstructOrderSearchDictionary(string xmlFilePath)
        {
            var resultList = new List<Result>(); //Plan is to return one Result per Order. Even if parsing of one Order Fails, Parser will proceed with other orders.

            if (!File.Exists(xmlFilePath))
            {
                resultList.Add(Result.GetFailedResult(ResultCodes.InputValidationFail, $"File path doesnt exist: {xmlFilePath}"));
                return resultList;
            }

            using (var xmlReader = XmlReader.Create(xmlFilePath))
            {
                xmlReader.MoveToContent();
                while (true)
                {
                    if (xmlReader.NodeType != XmlNodeType.Element || xmlReader.Name != "Order")
                    {
                        var readStatus = xmlReader.Read();
                        if (!readStatus) break;
                        continue;
                    }

                    var orderElement = XNode.ReadFrom(xmlReader) as XElement;
                    var parseOrderResult = _xmlMapper.GetOrderFromXml(orderElement, out Order parsedOrder);

                    resultList.Add(parseOrderResult);

                    if (parseOrderResult.OperationStatus == OperationStatus.Success)
                        OrderIdToOrder.Add(parsedOrder.Number.Value, parsedOrder);
                }
            }

            return resultList;
        }
    }
}
