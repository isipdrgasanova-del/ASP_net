using System.ComponentModel.DataAnnotations;

namespace WebApplication13.Models
{
    public class ValidPositionIdAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null) return new ValidationResult("Необходимо выбрать должность.");

            if (value is int positionId && positionId <= 0)
                return new ValidationResult(ErrorMessage ?? "Выбран некорректный идентификатор должности.");

            return ValidationResult.Success;
        }
    }
}