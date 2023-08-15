namespace EcoEkb.Backend.DataAccess.Domain.Exception;

public class UserFriendlyException : System.Exception
{
    public UserFriendlyException(string message = "Ошибка", System.Exception? innerException = null) 
        : base(message, innerException)
    {
    }
}