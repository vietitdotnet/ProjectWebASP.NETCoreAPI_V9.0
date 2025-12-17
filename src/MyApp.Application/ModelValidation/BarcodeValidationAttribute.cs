using System.ComponentModel.DataAnnotations;

namespace MyApp.Application.ModelValidation
{
    public class BarcodeValidationAttribute : ValidationAttribute
    {
        // Hàm kiểm tra mã vạch hợp lệ
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            // Cho phép mã vạch để trống
            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
            {
                return ValidationResult.Success;
            }

            string barcode = value.ToString();

            // Kiểm tra độ dài mã vạch (VD: mã vạch phải có 8, 12, hoặc 13 ký tự)
            if (!(barcode.Length == 8 || barcode.Length == 12 || barcode.Length == 13))
            {
                return new ValidationResult("Mã vạch phải có độ dài 8, 12 hoặc 13 ký tự.");
            }

            // Kiểm tra chỉ chứa chữ số
            if (!barcode.All(char.IsDigit))
            {
                return new ValidationResult("Mã vạch chỉ được chứa các chữ số.");
            }

            // Kiểm tra mã vạch bằng thuật toán kiểm tra checksum (VD: EAN-13)
            if (!IsValidEAN13(barcode))
            {
                return new ValidationResult("Mã vạch không hợp lệ theo thuật toán kiểm tra.");
            }

            return ValidationResult.Success;
        }

        // Hàm kiểm tra mã vạch EAN-13
        private bool IsValidEAN13(string barcode)
        {
            if (barcode.Length != 13) return true; // Không áp dụng cho mã ngắn hơn

            int sum = 0;

            for (int i = 0; i < barcode.Length - 1; i++)
            {
                int digit = int.Parse(barcode[i].ToString());
                if (i % 2 == 0)
                {
                    sum += digit;
                }
                else
                {
                    sum += digit * 3;
                }
            }

            int checksum = (10 - (sum % 10)) % 10;
            int lastDigit = int.Parse(barcode[^1].ToString());

            return checksum == lastDigit;
        }
    }
}
