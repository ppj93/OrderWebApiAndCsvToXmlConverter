using Common;
using System;
using System.Runtime.Serialization;

namespace Models.Response
{
    [DataContract]
    [Serializable]
    public class Response
    {
        [DataMember]
        public Result Result { get; set; }
    }
}
