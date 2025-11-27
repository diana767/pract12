using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace pract12.ValidationRules
{
    public class PasswordRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var password = (value ?? "").ToString();

            if (string.IsNullOrEmpty(password))
                return new ValidationResult(false, "Пароль обязателен");

            if (password.Length < 8)
                return new ValidationResult(false, "Пароль должен содержать минимум 8 символов");

            if (!Regex.IsMatch(password, @"[A-Z]"))
                return new ValidationResult(false, "Пароль должен содержать хотя бы одну заглавную букву");

            if (!Regex.IsMatch(password, @"[a-z]"))
                return new ValidationResult(false, "Пароль должен содержать хотя бы одну строчную букву");

            if (!Regex.IsMatch(password, @"\d"))
                return new ValidationResult(false, "Пароль должен содержать хотя бы одну цифру");

            if (!Regex.IsMatch(password, @"[!@#$%^&*()_+\-=\[\]{};':""\\|,.<>\/?]"))
                return new ValidationResult(false, "Пароль должен содержать хотя бы один специальный символ");

            return ValidationResult.ValidResult;
        }
    }
}
