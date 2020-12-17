using System.ComponentModel.DataAnnotations;

namespace Tomato.StandardLib.FrameWork
{
    public class RequiredAllAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value != null && !string.IsNullOrEmpty(value + string.Empty))
            {
                return true;
            }
            return false;
        }
    }
}
