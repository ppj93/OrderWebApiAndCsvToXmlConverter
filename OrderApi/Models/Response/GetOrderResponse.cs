using System;
using System.Runtime.Serialization;

namespace Models.Response
{
    [Serializable]
    [DataContract]
    public class GetOrderResponse: Response
    {
        [DataMember]
        public Order Order { get; set; }
    }
}
