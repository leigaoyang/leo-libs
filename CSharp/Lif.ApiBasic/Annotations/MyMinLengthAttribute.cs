using System.ComponentModel.DataAnnotations;

namespace Lif.ApiBasic.Annotations
{
    public class MyMinLengthAttribute : MinLengthAttribute
    {
        public MyMinLengthAttribute(int length) : base(length)
        {
            ErrorMessage = "{0}长度必须大于{1}";
        }
    }
}
