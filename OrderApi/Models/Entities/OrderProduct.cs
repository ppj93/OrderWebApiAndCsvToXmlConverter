using System;
using System.Runtime.Serialization;

namespace Models
{
    //Subclassed the type because base Product class can then be re-used to display Product details on Product Browsing page.
    [Serializable]
    [DataContract]
    public class OrderProduct: Product
    {
        [DataMember]
        public int? Quantity { get; set; }
    }
}
