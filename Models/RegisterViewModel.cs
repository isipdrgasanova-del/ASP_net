using System.ComponentModel.DataAnnotations;

namespace WebApplication12.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Логин обязателен")]
        [NoForbiddenWords(ErrorMessage = "Логин содержит запрещенные слова")]
        public string Login { get; set; } = null!;

        [Required(ErrorMessage = "Пароль обязателен")]
        [ComplexPassword(ErrorMessage = "Пароль слишком простой")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string ConfirmPassword { get; set; } = null!;

        [Required(ErrorMessage = "Имя обязательно")]
        [NoRepeatedCharacters(ErrorMessage = "Имя содержит слишком много повторяющихся символов")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Фамилия обязательна")]
        [NoRepeatedCharacters(ErrorMessage = "Фамилия содержит слишком много повторяющихся символов")]
        public string Surname { get; set; } = null!;

        [Required(ErrorMessage = "Выберите должность")]
        [ValidPositionId(ErrorMessage = "Некорректная должность")]
        public int IdPosition { get; set; }
    }
}