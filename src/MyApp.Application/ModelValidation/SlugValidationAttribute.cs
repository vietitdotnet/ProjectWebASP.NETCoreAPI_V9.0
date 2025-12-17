using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace MyApp.Application.ModelValidation
{
    public class SlugValidationAttribute : ValidationAttribute
    {
        // Biểu thức regex kiểm tra SLUG hợp lệ
        private static readonly Regex SlugRegex = new Regex(@"^[a-z0-9]+(-[a-z0-9]+)*$", RegexOptions.Compiled);

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            // Cho phép giá trị rỗng (nếu muốn bắt buộc thì dùng thêm [Required])
            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
            {
                return ValidationResult.Success;
            }

            string slug = value.ToString();

            // Kiểm tra SLUG với regex
            if (!SlugRegex.IsMatch(slug))
            {
                return new ValidationResult("Slug không hợp lệ. Slug chỉ được chứa các ký tự chữ cái thường, số, và dấu gạch ngang (-).");
            }

            return ValidationResult.Success;
        }
    }
}
