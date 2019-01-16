using Common;
using System;
using System.Runtime.Serialization;

namespace Models.Response
{
    [DataContract]
    [Serializable]
    public class Response
    {
        public Response(Result result)
        {
            Result = result;
        }

        [DataMember]
        public Result Result { get; set; }
    }
}
