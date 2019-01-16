using System;
using System.Runtime.Serialization;

namespace Common
{
    [DataContract]
    [Serializable]
    public enum OperationStatus
    {
        [DataMember]
        Success,
        [DataMember]
        Fail
    }
}
