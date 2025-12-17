using System.ComponentModel.DataAnnotations;

namespace MyApp.Application.ModelValidation
{
    public class PriceDiscountValidationAttribute : ValidationAttribute
    {
        public string PricePropertyName { get; }

        public PriceDiscountValidationAttribute(string pricePropertyName)
        {
            PricePropertyName = pricePropertyName;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var priceDiscount = value as decimal?;
            var priceProperty = validationContext.ObjectType.GetProperty(PricePropertyName);

            if (priceProperty == null)
            {
                return new ValidationResult($"Thuộc tính '{PricePropertyName}' không tồn tại.");
            }

            var price = priceProperty.GetValue(validationContext.ObjectInstance) as decimal?;

            if (price == null || price <= 0)
            {
                return new ValidationResult("Giá phải lớn hơn 0.");
            }

            if (priceDiscount.HasValue)
            {
                if (priceDiscount.Value <= 0 || priceDiscount.Value >= price)
                {
                    return new ValidationResult("Giảm giá phải lớn hơn 0 và nhỏ hơn giá gốc.");
                }
            }

            return ValidationResult.Success;
        }
    }
}
