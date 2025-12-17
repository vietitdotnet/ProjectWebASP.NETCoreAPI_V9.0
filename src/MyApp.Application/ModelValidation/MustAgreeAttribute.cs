using System.ComponentModel.DataAnnotations;

namespace MyApp.Application.ModelValidation
{
    public class MustAgreeAttribute : ValidationAttribute
    {
        public MustAgreeAttribute()
        {
            ErrorMessage = "Bạn phải đồng ý với chính sách giao hàng.";
        }

        public override bool IsValid(object value)
        {
            if (value is bool boolValue)
            {
                return boolValue; // Chỉ hợp lệ nếu giá trị là true
            }
            return false;
        }
    }
}
