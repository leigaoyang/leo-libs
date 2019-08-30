using System.ComponentModel.DataAnnotations;

namespace Lif.ApiBasic.Annotations
{
    public class MyMaxLengthAttribute : MaxLengthAttribute
    {
        public MyMaxLengthAttribute() : base()
        {
            ErrorMessage = "{0}长度必须小于{1}";
        }

        public MyMaxLengthAttribute(int length) : base(length)
        {
            ErrorMessage = "{0}长度必须小于{1}";
        }
    }
}
