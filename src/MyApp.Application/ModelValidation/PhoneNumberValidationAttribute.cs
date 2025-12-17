using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace MyApp.Application.ModelValidation
{
    public class PhoneNumberValidationAttribute : ValidationAttribute
    {
        private const string DefaultErrorMessage = "Số điện thoại không hợp lệ.";

        // Mẫu regex kiểm tra số điện thoại
        private const string PhoneNumberPattern = @"^\+?\d{10,15}$";

        public PhoneNumberValidationAttribute() : base(DefaultErrorMessage)
        {
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
            {
                return ValidationResult.Success; // Không kiểm tra nếu giá trị null
            }

            var phoneNumber = value.ToString();
            if (Regex.IsMatch(phoneNumber, PhoneNumberPattern))
            {
                return ValidationResult.Success; // Hợp lệ
            }

            return new ValidationResult(ErrorMessage ?? DefaultErrorMessage);
        }
    }
}
