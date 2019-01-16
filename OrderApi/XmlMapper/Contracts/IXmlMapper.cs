using Common;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
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
