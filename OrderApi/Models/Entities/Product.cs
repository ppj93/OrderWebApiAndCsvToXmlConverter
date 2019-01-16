using System;
using System.Runtime.Serialization;

namespace Models
{
    [Serializable]
    [DataContract]
    public class Product
    {
        [DataMember]
        public string Number { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public decimal? Price { get; set; }

        [DataMember]
        public string Group { get; set; }
    }
}
