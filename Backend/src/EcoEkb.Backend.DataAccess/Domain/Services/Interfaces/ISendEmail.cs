using System.Threading.Tasks;

namespace EcoEkb.Backend.DataAccess.Domain.Services.Interfaces;

public interface ISendEmail
{
    public Task SendEmailAsync(string emailTo, string subject, string message);
}