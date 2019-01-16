using Common;
using System.Collections.Generic;

namespace OrderManager.Contract
{
    public interface IOrderManager
    {
        List<Result> ConstructOrderSearchDictionary(string xmlFilePath);
    }
}
