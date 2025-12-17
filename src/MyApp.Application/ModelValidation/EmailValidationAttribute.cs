using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace MyApp.Application.ModelValidation
{
    public class EmailValidationAttribute : ValidationAttribute
    {
        private const string DefaultErrorMessage = "Địa chỉ email không hợp lệ.";

        // Mẫu regex kiểm tra email
        private const string EmailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

        public EmailValidationAttribute() : base(DefaultErrorMessage)
        {
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
            {
                return ValidationResult.Success; // Không kiểm tra nếu giá trị null
            }

            var email = value.ToString();
            if (Regex.IsMatch(email, EmailPattern))
            {
                return ValidationResult.Success; // Hợp lệ
            }

            return new ValidationResult(ErrorMessage ?? DefaultErrorMessage);
        }

    }
}
