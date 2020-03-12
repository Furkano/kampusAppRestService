using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace noname.DTO
{
    public class ResponceModel
    {
        public int StatusCode { get; set; }
        public string StatusMessage { get; set; }
        public object Result { get; set; }
        public string[] Errors { get; set; }
        public ResponceModel(int statusCode,string statusMessage,object result,string[] errors)
        {
            StatusCode = statusCode;
            StatusMessage = statusMessage;
            Result = result;
            Errors = errors;
        }
    }
}
