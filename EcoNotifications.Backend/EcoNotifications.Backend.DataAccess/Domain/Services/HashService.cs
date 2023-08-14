using System.Security.Cryptography;
using System.Text;
using EcoNotifications.Backend.DataAccess.Services.Interfaces;

namespace EcoNotifications.Backend.DataAccess.Domain.Services;

public class HashService : IHashService
{
    private static readonly byte[] salt = Encoding.UTF8.GetBytes("Pugnare aut Mori ! ! !");

    /// <summary>
    /// Encrypts the user's password
    /// </summary>
    /// <param name="password">Unencrypted password of the user</param>
    /// <returns>Encrypted password of the user</returns>
    public string EncryptPassword(string password)
    {
        var byteResult = new Rfc2898DeriveBytes(Encoding.UTF8.GetBytes(password), 
            salt, 9099, HashAlgorithmName.SHA256);
        return Convert.ToBase64String(byteResult.GetBytes(32));
    }
}