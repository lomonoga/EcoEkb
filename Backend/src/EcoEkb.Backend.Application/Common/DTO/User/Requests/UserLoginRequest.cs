namespace EcoEkb.Backend.Application.Common.DTO.User.Requests;

public class UserLoginRequest
{
    public string? Email { get; set; }
    public string? Password { get; set; }
    public string? Token { get; set; }
    public string? RefreshToken { get; set; }

}