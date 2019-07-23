using System.Collections.Generic;
using Lif.ApiBasic.Interface;

namespace Lif.ApiBasic
{
    public class ApiResult : IApiResult
    {
        public ApiState State { get; set; }
        public string Msg { get; set; }
        public Dictionary<string, string> Errors { get; set; }
        public object Data { get; set; }
    }
}
