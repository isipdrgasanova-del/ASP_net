using System.ComponentModel.DataAnnotations;

namespace WebApplication13.Models
{
    public class NoRepeatedCharactersAttribute : ValidationAttribute
    {
        private readonly int _maxRepeats = 3;

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null) return ValidationResult.Success;
            string text = value.ToString()!;
            int count = 1;

            for (int i = 1; i < text.Length; i++)
            {
                if (text[i] == text[i - 1])
                {
                    count++;
                    if (count > _maxRepeats)
                        return new ValidationResult(ErrorMessage ?? $"Недопустимо повторение одного символа более {_maxRepeats} раз подряд.");
                }
                else count = 1;
            }
            return ValidationResult.Success;
        }
    }
}