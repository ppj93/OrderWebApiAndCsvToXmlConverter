﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Result {
        public OperationStatus OperationStatus { get; set; }
        public string Code { get; set; }
        public string Message { get; set; }

        public static Result GetFailedResult(string code, string message) {
            return new Result() { OperationStatus = OperationStatus.Fail, Code = code, Message = message };
        }
    }
}
