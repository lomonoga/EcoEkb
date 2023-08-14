namespace EcoNotifications.Backend.DataAccess.Services.Interfaces;

public interface IHashService
{
    public string EncryptPassword(string password);
}