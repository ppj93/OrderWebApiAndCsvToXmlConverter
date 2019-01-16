using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace OrderXmlMapper.Contracts
{
    public interface IOrderMapper
    {
        Result GetOrderElement(IList<string[]> orderItems, out XElement orderElement);
    }
}
