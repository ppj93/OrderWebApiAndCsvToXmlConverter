using Common;
using OrderManager.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManager
{
    public class OrderManager : IOrderManager
    {
        public Result ConstructOrderSearchDictionary(string xmlFilePath)
        {
            return new Result() { OperationStatus = OperationStatus.Success };
        }
    }
}
