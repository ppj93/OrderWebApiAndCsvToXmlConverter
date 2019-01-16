using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Common;
using Models;
using XmlMapper.Contracts;

namespace XmlMapper
{
    public class XmlMapper : IXmlMapper
    {
        public Result GetCustomerFromXml(XElement root, out Customer customer)
        {
            var result = new Result() { OperationStatus = OperationStatus.Success };
            customer = null;

            if (root == null || root.Nodes().Count() == 0)
            {
                //Log failed response here.
                return Result.GetFailedResult(ResultCodes.InputValidationFail, "Customer root node is null or has no child nodes");
            }
            customer = new Customer();

            foreach (XElement desc in root.Nodes())
            {
                if (desc.Name == XmlNodeNames.Customer.Name)
                    customer.Name = desc.Value;
                else if (desc.Name == XmlNodeNames.Customer.Number)
                    customer.Number = Utilties.SafeParse<long>(desc.Value, long.TryParse);
            }

            if (!customer.Number.HasValue)
                result = Result.GetFailedResult(ResultCodes.ParseFail, "Customer Number could not be parsed");

            return result;
        }


        public Result GetOrderFromXml(XElement root, out Order order)
        {
            var result = new Result() { OperationStatus = OperationStatus.Success };
            order = null;

            if (root == null || root.Nodes().Count() == 0)
            {
                //Log failed response here.
                return Result.GetFailedResult(ResultCodes.InputValidationFail, "Order root node is null or has no child nodes");
            }
            order = new Order();

            foreach (XElement desc in root.Nodes())
            {
                if (desc.Name == XmlNodeNames.Orders.Date)
                    order.Date = DateTime.Parse(desc.Value); // TODO: replace by try parse
                else if (desc.Name == XmlNodeNames.Orders.Products)
                { 
                    result = GetProductListFromXml(desc, out List<OrderProduct> productList);
                    order.Products = productList;
                }
                else if (desc.Name == XmlNodeNames.Orders.Number)
                {
                    order.Number = Utilties.SafeParse<long>(desc.Value, long.TryParse);
                    if (!order.Number.HasValue)
                        result = Result.GetFailedResult(ResultCodes.ParseFail, $"Order Number failed to parse {root.ToString()}");
                }
                else if (desc.Name == XmlNodeNames.Customer.Title)
                {
                    result = GetCustomerFromXml(desc, out Customer customer);
                    order.CustomerDetails = customer;
                }

                if (result.OperationStatus != OperationStatus.Success)
                    return result;
            }

            return result;
        }

        public Result GetProductFromXml(XElement root, out OrderProduct product)
        {
            var result = new Result() { OperationStatus = OperationStatus.Success };
            product = null;

            if (root == null || root.Nodes().Count() == 0)
            {
                //Log failed response here.
                return Result.GetFailedResult(ResultCodes.InputValidationFail, "Customer root node is null or has no child nodes");
            }
            product = new OrderProduct();

            foreach (XElement desc in root.Nodes())
            {
                if (desc.Name == XmlNodeNames.Product.Name)
                    product.Name = desc.Value;
                else if (desc.Name == XmlNodeNames.Product.Number)
                    product.Number = desc.Value;
                else if (desc.Name == XmlNodeNames.Product.Quantity) { 
                    product.Quantity = Utilties.SafeParse<int>(desc.Value, int.TryParse);
                    if (!product.Quantity.HasValue)
                        result = Result.GetFailedResult(ResultCodes.ParseFail, $"Product Quantity failed to parse {root.ToString()}");
                }
                else if (desc.Name == XmlNodeNames.Product.Group)
                    product.Group = desc.Value;
                else if (desc.Name == XmlNodeNames.Product.Price) { 
                    product.Price = Utilties.SafeParse<decimal>(desc.Value, decimal.TryParse);
                    if(!product.Price.HasValue)
                        result = Result.GetFailedResult(ResultCodes.ParseFail, $"Product Price failed to parse {root.ToString()}");
                }
                else if (desc.Name == XmlNodeNames.Product.Description)
                    product.Description = desc.Value;

                if (result.OperationStatus != OperationStatus.Success)
                    return result;
            }

            return result;
        }

        public Result GetProductListFromXml(XElement root, out List<OrderProduct> productList)
        {
            var result = new Result() { OperationStatus = OperationStatus.Success };
            productList = null;

            if (root == null || root.Nodes().Count() == 0)
            {
                //Log failed response here.
                return Result.GetFailedResult(ResultCodes.InputValidationFail, "Product List root node is null or has no child nodes");
            }
            productList = new List<OrderProduct>();

            foreach (var productNode in root.Nodes())
            {
                result = GetProductFromXml(productNode as XElement, out OrderProduct parsedProduct);
                if (result.OperationStatus != OperationStatus.Success) return result;
                productList.Add(parsedProduct);
            }

            return result;
        }
    }
}
