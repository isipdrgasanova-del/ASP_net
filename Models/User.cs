using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApplication13.Models;

public partial class User
{
    public int IdUser { get; set; }

    [Required(ErrorMessage = "Логин обязателен для заполнения.")]
    [NoForbiddenWords(ErrorMessage = "Этот логин использовать нельзя, так как он содержит защищенные системные слова.")]
    public string Login { get; set; } = null!;

    [Required(ErrorMessage = "Пароль обязателен для заполнения.")]
    [ComplexPassword(ErrorMessage = "Пароль слишком простой. Требуются заглавные и строчные буквы, цифры и спецсимволы.")]
    public string Password { get; set; } = null!;

    [Required(ErrorMessage = "Фамилия обязательна.")]
    [NoRepeatedCharacters(ErrorMessage = "Фамилия содержит слишком много повторяющихся подряд символов.")]
    public string Surname { get; set; } = null!;

    [Required(ErrorMessage = "Имя обязательно.")]
    [NoRepeatedCharacters(ErrorMessage = "Имя содержит слишком много повторяющихся подряд символов.")]
    public string Name { get; set; } = null!;

    public string? MiddleName { get; set; }

    [ValidPositionId(ErrorMessage = "Пожалуйста, выберите корректную должность из списка.")]
    public int IdPosition { get; set; }

    [RegistrationDateRange(ErrorMessage = "Неверная дата регистрации пользователя.")]
    public DateOnly RegistrationDate { get; set; }

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();
    public virtual Position IdPositionNavigation { get; set; } = null!;
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}