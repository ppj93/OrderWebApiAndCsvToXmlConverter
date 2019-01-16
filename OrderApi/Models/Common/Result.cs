using System;
using System.Runtime.Serialization;

namespace Common
{
    [DataContract]
    [Serializable]
    public class Result {
        [DataMember]
        public OperationStatus OperationStatus { get; set; }

        [DataMember]
        public string Code { get; set; }

        [DataMember]
        public string Message { get; set; }

        public static Result GetSuccessResult() {
            return new Result() { OperationStatus = OperationStatus.Success };
        }

        public static Result GetFailedResult(string code, string message) {
            return new Result() { OperationStatus = OperationStatus.Fail, Code = code, Message = message };
        }
    }
}
