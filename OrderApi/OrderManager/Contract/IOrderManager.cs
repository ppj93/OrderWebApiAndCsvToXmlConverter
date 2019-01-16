using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManager.Contract
{
    public interface IOrderManager
    {
        List<Result> ConstructOrderSearchDictionary(string xmlFilePath);
    }
}
