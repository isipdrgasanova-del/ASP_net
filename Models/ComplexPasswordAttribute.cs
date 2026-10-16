using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace WebApplication12.Models
{
    public class ComplexPasswordAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null) return ValidationResult.Success;
            string password = value.ToString()!;

            bool hasUpperCase = Regex.IsMatch(password, @"[A-ZА-Я]");
            bool hasLowerCase = Regex.IsMatch(password, @"[a-zа-я]");
            bool hasDigits = Regex.IsMatch(password, @"[0-9]");
            bool hasSpecialChar = Regex.IsMatch(password, @"[\W_]");

            if (!hasUpperCase || !hasLowerCase || !hasDigits || !hasSpecialChar)
            {
                return new ValidationResult(ErrorMessage ?? "Пароль должен содержать заглавные и строчные буквы, цифры и специальные символы.");
            }
            return ValidationResult.Success;
        }
    }
}