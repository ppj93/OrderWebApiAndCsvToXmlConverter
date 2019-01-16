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
        private readonly IDictionary<long, Order> OrderIdToOrder;

        public OrderManager() : this(new Dictionary<long, Order>()) { }

        //Constructor for unit testing purposes
        public OrderManager(IDictionary<long, Order> orderIdToFileOffset)
        {
            OrderIdToOrder = orderIdToFileOffset;
        }

        public Result ConstructOrderSearchDictionary(string xmlFilePath)
        {
            long offset = 0;
            //TODO: validate path, check dictionary is empty
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
                    var createOrderFromXml = CreateOrderFromXml(orderElement);
                    OrderIdToOrder.Add(createOrderFromXml.Number, createOrderFromXml);
                }
            }

            return new Result() { OperationStatus = OperationStatus.Success };
        }

        private static Order CreateOrderFromXml(XElement orderNode)
        {
            //TODO: VALIDATE INPUT

            var order = new Order();
            foreach (XElement desc in orderNode.Nodes())
            {
                if (desc.Name == XmlNodeNames.Orders.Date)
                    order.Date = DateTime.Parse(desc.Value); // TODO: replace by try parse
                else if (desc.Name == XmlNodeNames.Orders.Products)
                    order.Products = CreateProductListFromXml(desc);
                else if (desc.Name == XmlNodeNames.Orders.Number)
                    order.Number = long.Parse(desc.Value);
                else if (desc.Name == XmlNodeNames.Customer.Title)
                    order.CustomerDetails = CreateCustomerFromXml(desc);
            }

            return order;
        }

        private static List<Product> CreateProductListFromXml(XElement productListNode)
        {
            var productList = new List<Product>();
            foreach (var product in productListNode.Nodes())
                productList.Add(CreateProductFromXml(product as XElement));
            return productList;
        }

        private static OrderProduct CreateProductFromXml(XElement root)
        {
            var product = new OrderProduct();

            foreach (XElement desc in root.Nodes())
            {
                if (desc.Name == XmlNodeNames.Product.Name)
                    product.Name = desc.Value;
                else if (desc.Name == XmlNodeNames.Product.Number)
                    product.Number = desc.Value;
                else if (desc.Name == XmlNodeNames.Product.Quantity)
                    product.Quantity = int.Parse(desc.Value);
                else if (desc.Name == XmlNodeNames.Product.Group)
                    product.Group = desc.Value;
                else if (desc.Name == XmlNodeNames.Product.Price)
                    product.Price = decimal.Parse(desc.Value);
                else if (desc.Name == XmlNodeNames.Product.Description)
                    product.Description = desc.Value;
            }
            return product;
        }
        private static Customer CreateCustomerFromXml(XElement root)
        {
            var customer = new Customer();

            foreach (XElement desc in root.Nodes())
            {
                if (desc.Name == XmlNodeNames.Customer.Name)
                    customer.Name = desc.Value;
                else if (desc.Name == XmlNodeNames.Customer.Number)
                    customer.Number = long.Parse(desc.Value); // replace by try parse
            }

            return customer;
        }
    }
}
