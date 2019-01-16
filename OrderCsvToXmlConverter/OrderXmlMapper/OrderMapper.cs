using Common;
using OrderXmlMapper.Contracts;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace OrderXmlMapper
{
    public class OrderMapper : IOrderMapper
    {
        
        public Result GetOrderElement(IList<string[]> orderItems, out XElement orderElement)
        {
            var result = new Result() { OperationStatus = OperationStatus.Success };

            orderElement = null;

            if(!orderItems.Any())
            {
                result.Code = ResultCodes.InputValidationFail;
                result.Message = "OrderItems is empty";
                result.OperationStatus = OperationStatus.Fail;
                return result;
            }

            if (orderItems.Any(orderItem => orderItem.Length < 13))
            {
                result.Code = ResultCodes.InputValidationFail;
                result.Message = "CSV possibly contains less than 11 columsn.. possible column mismatch";
                result.OperationStatus = OperationStatus.Fail;
                return result;
            }

            //Line Number ignored because it is a serial number 
            orderElement = new XElement(XmlNodeNames.Orders.Order,
                new XElement(XmlNodeNames.Orders.Date, orderItems.First()[9]),
                new XElement(XmlNodeNames.Orders.Number, orderItems.First()[1]));

            var productsElement = new XElement(XmlNodeNames.Orders.Products);

            foreach (var order in orderItems)
            {
                var productElement = GetProductFromOrderRow(order);
                productsElement.Add(productElement);
            }

            orderElement.Add(productsElement);
            orderElement.Add(GetCustomerFromOrderRow(orderItems.First()));

            return result;
        }

        private static XElement GetProductFromOrderRow(string[] values)
        {
            return new XElement(XmlNodeNames.Product.Title,
                new XElement(XmlNodeNames.Product.Name, values[5]),
                new XElement(XmlNodeNames.Product.Price, values[7]),
                new XElement(XmlNodeNames.Product.Group, values[8]),
                new XElement(XmlNodeNames.Product.Number, values[3]),
                new XElement(XmlNodeNames.Product.Quantity, values[4]),
                new XElement(XmlNodeNames.Product.Description, values[6])
                );
        }

        private static XElement GetCustomerFromOrderRow(string[] values)
        {
            return new XElement(XmlNodeNames.Customer.Title,
                new XElement(XmlNodeNames.Customer.Name, values[10]),
                new XElement(XmlNodeNames.Product.Number, values[11])
                );
        }
    }
}
