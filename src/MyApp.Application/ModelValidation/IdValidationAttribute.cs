using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace MyApp.Application.ModelValidation
{
    public class IdValidationAttribute : ValidationAttribute
    {
             // Hàm kiểm tra tính hợp lệ của ID
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            // Cho phép ID để trống (nếu không bắt buộc)
            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
            {
                return ValidationResult.Success;
            }

            string id = value.ToString();

            // Kiểm tra ID chỉ chứa chữ cái từ a-z và chữ số
            if (!IsValidID(id))
            {
                return new ValidationResult("ID chỉ được chứa chữ số và ký tự từ a-z (chữ cái thường và viết hoa).");
            }

            return ValidationResult.Success;
        }


        private bool IsValidID(string input)
        {
            // Biểu thức chính quy: ^[a-z0-9-]+$
            Regex regex = new Regex("^[a-zA-Z0-9-]+$");
            return regex.IsMatch(input);
        }

    }
}
