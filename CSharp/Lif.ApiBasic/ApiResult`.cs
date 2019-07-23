using System.Collections.Generic;
using Lif.ApiBasic.Interface;

namespace Lif.ApiBasic
{
    public class ApiResult<T> : IApiResult
    {
        public ApiState State { get; set; }
        public string Msg { get; set; }
        public Dictionary<string, string> Errors { get; set; }
        public T Data { get; set; }


        public static implicit operator ApiResult<T>(ApiResult result)
        {
            return new ApiResult<T>
            {
                State = result.State,
                Msg = result.Msg,
                Data = result.Data is T t ? t : default,
                Errors = result.Errors
            };
        }
        public static implicit operator ApiResult<T>(T data)
        {
            return new ApiResult<T>
            {
                State = ApiState.Success,
                Msg = "成功",
                Data = data
            };
        }
    }
}
