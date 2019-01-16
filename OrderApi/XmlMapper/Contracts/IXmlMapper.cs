using Common;
using Models;
using System.Collections.Generic;
using System.Xml.Linq;

namespace XmlMapper.Contracts
{
    public interface IXmlMapper
    {
        Result GetOrderFromXml(XElement node, out Order order); 
        Result GetProductFromXml(XElement node, out OrderProduct order); 
        Result GetCustomerFromXml(XElement node, out Customer order);
        Result GetProductListFromXml(XElement node, out List<OrderProduct> order);
    }
}
