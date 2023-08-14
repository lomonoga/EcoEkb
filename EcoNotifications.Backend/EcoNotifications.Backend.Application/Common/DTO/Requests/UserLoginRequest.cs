using System.ComponentModel.DataAnnotations;

namespace EcoNotifications.Backend.Application.Common.DTO;

public class UserLoginRequest
{
    [EmailAddress(ErrorMessage = "Такая электронная почта недействительна")]
    public string Email { get; set; }
    public string Password { get; set; }
}