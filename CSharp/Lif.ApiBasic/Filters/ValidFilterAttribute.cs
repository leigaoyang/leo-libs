using System.Collections.Generic;
using System.Linq;
using Lif.ApiBasic.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Lif.ApiBasic.Filters
{
    public class ValidFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ModelState.IsValid)
            {
                return;
            }

            var errors = new Dictionary<string, string>();

            foreach (var item in context.ModelState)
            {
                errors[item.Key.ToCamelCase()] = item.Value.Errors.FirstOrDefault()?.ErrorMessage;
            }

            context.Result = new ObjectResult(new ApiResult
            {
                State = ApiState.Invalid,
                Msg = "失败!",
                Errors = errors
            });
            base.OnActionExecuting(context);
        }
    }
}
