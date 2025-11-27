using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace pract12.ValidationRules
{
    public class MinLengthRule : ValidationRule
    {
        public int MinLength { get; set; } = 5;

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var input = (value ?? "").ToString();
            if (input.Length < MinLength)
            {
                return new ValidationResult(false, $"Минимальная длина: {MinLength} символов");
            }
            return ValidationResult.ValidResult;
        }
    }
}
