using System.ComponentModel.DataAnnotations;

namespace Lif.ApiBasic.Annotations
{
    public class MyRegexAttribute : RegularExpressionAttribute
    {
        public MyRegexAttribute(string pattern) : base(pattern)
        {
            ErrorMessage = "{0}格式不正确!";
        }
    }
}
