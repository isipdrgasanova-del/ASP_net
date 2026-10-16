using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace WebApplication12.Models
{
    public class NoForbiddenWordsAttribute : ValidationAttribute
    {
        private readonly string[] _forbiddenWords = { "admin", "root", "test", "moderator", "administrator" };

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null) return ValidationResult.Success;
            string stringValue = value.ToString()!.ToLower();

            if (_forbiddenWords.Any(word => stringValue.Contains(word)))
            {
                return new ValidationResult(ErrorMessage ?? "Значение содержит запрещенные системные слова.");
            }
            return ValidationResult.Success;
        }
    }
}