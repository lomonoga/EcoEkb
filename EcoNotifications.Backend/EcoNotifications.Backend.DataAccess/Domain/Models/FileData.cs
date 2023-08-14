namespace EcoNotifications.Backend.DataAccess.Domain.Models;

public class FileData
{
    public string Name { get; set; }
    public byte[] Content { get; set; }
    public string Type { get; set; }
}