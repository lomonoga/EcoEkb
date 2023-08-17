﻿using System.ComponentModel.DataAnnotations;

namespace EcoEkb.Backend.Application.Common.DTO.User.Requests;

public class UserEditRequest
{
    [Required(ErrorMessage = "Не указано ФИО")]
    public string? FullName { get; set; }
    
    [StringLength(11, MinimumLength = 11, ErrorMessage = "Номер должен состоять из 11 цифр")]
    [RegularExpression(@"^[78]\d+", ErrorMessage = "Некорректный формат телефона")]
    public string? Phone { get; set; }
    
    [StringLength(250, MinimumLength = 10, ErrorMessage = "Пароль должно быть не менее 10 символов")]
    public string? Password { get; set; }
}