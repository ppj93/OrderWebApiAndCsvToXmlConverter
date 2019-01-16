using Common;
using Models;
using OrderManager.Contract;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using XmlMapper.Contracts;

namespace OrderManager
{
    public class OrderManager : IOrderManager
    {
        private readonly IXmlMapper _xmlMapper;
     
        private readonly IDictionary<long, Order> _orderIdToOrderMap;

        //Constructor for unit testing purposes
        public OrderManager(IXmlMapper xmlMapper, IDictionary<long, Order> orderIdToFileOffset=null)
        {
            _orderIdToOrderMap = orderIdToFileOffset ?? new Dictionary<long, Order>();
            _xmlMapper = xmlMapper;
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
                        _orderIdToOrderMap.Add(parsedOrder.Number.Value, parsedOrder);
                }
            }

            return resultList;
        }
    }
}
