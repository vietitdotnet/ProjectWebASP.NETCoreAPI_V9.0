using System.ComponentModel.DataAnnotations;

namespace MyApp.Application.ModelValidation
{
    public class ColumExcelAttribute  : ValidationAttribute
    {
        private readonly int _maxUpperCaseCount;

        public ColumExcelAttribute(int maxUpperCaseCount = 2)
        {
            _maxUpperCaseCount = maxUpperCaseCount;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is string str)
            {
                // Kiểm tra chuỗi chỉ chứa các ký tự từ A đến Z
                if (!str.All(c => char.IsLetter(c) && c >= 'A' && c <= 'Z' || c >= 'a' && c <= 'z'))
                {
                    return new ValidationResult("Chuỗi chỉ được chứa các ký tự từ A đến Z.");
                }

                // Đếm số lượng ký tự viết hoa
                int upperCaseCount = str.Count(char.IsUpper);

                if (upperCaseCount > _maxUpperCaseCount)
                {
                    return new ValidationResult($"Chuỗi chỉ được chứa tối đa {_maxUpperCaseCount} ký tự viết hoa.");
                }

                return ValidationResult.Success;
            }

            return new ValidationResult("Giá trị không hợp lệ.");
        }
    }
}
