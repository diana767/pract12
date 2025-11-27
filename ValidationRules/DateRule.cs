using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace pract12.ValidationRules
{
    public class DateRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value is DateTime date)
            {
                if (date > DateTime.Now)
                    return new ValidationResult(false, "Дата создания не может быть в будущем");
            }

            return ValidationResult.ValidResult;
        }
    }
}
