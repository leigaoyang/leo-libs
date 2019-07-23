using System.Collections.Generic;
using System.Linq;
using Lif.ApiBasic.Extensions;

namespace Lif.ApiBasic
{
    public abstract class BaseService
    {
        protected ApiResult Success(string msg = "成功!")
        {
            return new ApiResult
            {
                State = ApiState.Success,
                Msg = msg
            };
        }

        protected ApiResult Fail(string msg = "失败!")
        {
            return new ApiResult
            {
                State = ApiState.Fail,
                Msg = msg
            };
        }

        protected ApiResult Invalid(params (string name, string msg)[] errors)
        {
            return Invalid("失败!", errors);
        }

        protected ApiResult Invalid(string msg, params (string name, string msg)[] errors)
        {
            return new ApiResult
            {
                State = ApiState.Invalid,
                Msg = msg,
                Errors = new Dictionary<string, string>(errors.Select(o => new KeyValuePair<string, string>(o.name.ToCamelCase(), o.msg)))
            };
        }
    }
}
