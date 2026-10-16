using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication12.Models
{
    public class RegistrationDateRangeAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null) return ValidationResult.Success;

            if (value is DateOnly registrationDate)
            {
                DateOnly today = DateOnly.FromDateTime(DateTime.Today);

                if (registrationDate > today)
                {
                    return new ValidationResult(ErrorMessage ?? "Дата регистрации не может быть в будущем.");
                }
                if (registrationDate < today)
                {
                    return new ValidationResult(ErrorMessage ?? "Дата регистрации должна соответствовать сегодняшнему дню.");
                }
            }
            return ValidationResult.Success;
        }
    }
}