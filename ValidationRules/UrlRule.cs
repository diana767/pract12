using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace pract12.ValidationRules
{
    public class UrlRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var url = (value ?? "").ToString();

            if (string.IsNullOrEmpty(url))
                return ValidationResult.ValidResult;

            if (!Uri.TryCreate(url, UriKind.Absolute, out Uri uriResult) ||
                !(uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps))
            {
                return new ValidationResult(false, "Некорректный URL. Пример: http://example.com");
            }

            return ValidationResult.ValidResult;
        }
    }
}