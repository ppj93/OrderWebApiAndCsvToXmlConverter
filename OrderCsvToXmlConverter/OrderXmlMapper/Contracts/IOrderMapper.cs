using Common;
using System.Collections.Generic;
using System.Xml.Linq;

namespace OrderXmlMapper.Contracts
{
    public interface IOrderMapper
    {
        Result GetOrderElement(IList<string[]> orderItems, out XElement orderElement);
    }
}
