using Common;
using Models;
using System.Collections.Generic;

namespace OrderManager.Contract
{
    public interface IOrderManager
    {
        List<Result> ConstructOrderSearchDictionary(string xmlFilePath);
        Result Get(long orderNumber, out Order order);
    }
}
