using Common;
using Models;
using OrderManager.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace OrderManager
{
    public class OrderManager : IOrderManager
    {
        private readonly IDictionary<long, long> OrderIdToFileOffset;

        public OrderManager() : this(new Dictionary<long, long>()) { }

        //Constructor for unit testing purposes
        public OrderManager(IDictionary<long, long> orderIdToFileOffset)
        {
            OrderIdToFileOffset = orderIdToFileOffset;
        }

        public Result ConstructOrderSearchDictionary(string xmlFilePath)
        {
            long offset = 0;
            //TODO: validate path, check dictionary is empty
            using (var xmlReader = XmlReader.Create(xmlFilePath))
            {
                xmlReader.MoveToContent();
                while (xmlReader.Read())
                {
                    if (xmlReader.Name != "Order") continue;

                    var orderElement = XNode.ReadFrom(xmlReader) as XElement;
                    var createOrderFromXml = createOrderFromXml(orderElement);
                    OrderIdToFileOffset.Add(xmlReader.Value)
                    xmlReader.Skip();
                }
            }
        }

        private static Order CreateOrderFromXml(XElement orderNode) {
            //TODO: VALIDATE INPUT

            var order = new Order();
            foreach (var desc in orderNode.Descendants()) {
                if (desc.Name == XmlNodeNames.Orders.Date)
                    order.Date = DateTime.Parse(desc.Value); // TODO: replace by try parse
                else if (desc.Name == XmlNodeNames.Orders.Products)
                    order.Products = CreateProductListFromXml(desc);
            }
        }
    }
}
