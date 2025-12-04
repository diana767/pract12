using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace pract12.ValidationRules
{
    public class PhoneRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var phone = (value ?? "").ToString();

            if (string.IsNullOrEmpty(phone))
                return ValidationResult.ValidResult; // Телефон не обязателен

            if (!Regex.IsMatch(phone, @"^\+?[1-9]\d{1,14}$"))
                return new ValidationResult(false, "Некорректный формат телефона");

            return ValidationResult.ValidResult;
        }
    }
}