using System.ComponentModel.DataAnnotations;

namespace Lif.ApiBasic.Annotations
{
    public class MyRequiredAttribute : RequiredAttribute
    {
        public MyRequiredAttribute()
        {
            ErrorMessage = "{0}不能为空!";
        }
    }
}
