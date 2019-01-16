using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Models
{
    [Serializable]
    [DataContract]
    public class Order
    { 
        [DataMember]
        public long? Number { get; set; } //Could have named it OrderNumber. But since it is alraedy inside Order Class, Order prefix feels redundant

        [DataMember]
        public List<OrderProduct> Products { get; set; }

        [DataMember]
        public DateTime? Date { get; set; }

        [DataMember]
        public Customer CustomerDetails { get; set; }
    }
}
