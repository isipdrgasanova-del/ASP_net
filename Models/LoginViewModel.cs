using System.ComponentModel.DataAnnotations;

namespace WebApplication12.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Введите логин")]
        public string Login { get; set; } = null!;

        [Required(ErrorMessage = "Введите пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;
    }
}