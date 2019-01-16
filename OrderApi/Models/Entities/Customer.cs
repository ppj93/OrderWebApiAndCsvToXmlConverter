using System;
using System.Runtime.Serialization;

namespace Models
{
    [Serializable]
    [DataContract]
    public class Customer
    {
        [DataMember]
        public long? Number { get; set; }

        [DataMember]
        public string Name { get; set; }
    }
}
