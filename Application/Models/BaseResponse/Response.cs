using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.BaseResponse
{
    public class Response<T> : BaseResponse
    {
        public T? Data { get; set; }
        public List<T> DataList { get; set; } = new();
        public int TotalRows { get; set; }
    }
    public class BaseResponse
    {
        public bool IsSuccess { get; set; } = true;
        public string MessageTitle { get; set; }
        public string ErrorMessage { get; set; }
        public int StatusCode { get; set; } = (int)HttpStatusCode.OK;
        public List<string> WarningMessages { get; set; } = new();
    }
}
