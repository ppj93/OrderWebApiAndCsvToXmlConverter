using Common;
using System;
using System.Runtime.Serialization;

namespace Models.Response
{
    [Serializable]
    [DataContract]
    public class GetOrderResponse: Response
    {
        public GetOrderResponse(Result result, Order order=null):base(result)
        {
            Order = order;
        }

        [DataMember]
        public Order Order { get; set; }
    }
}
