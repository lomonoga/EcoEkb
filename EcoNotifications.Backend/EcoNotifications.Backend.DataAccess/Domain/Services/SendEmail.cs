using System.Net;
using System.Net.Mail;
using EcoNotifications.Backend.DataAccess.Domain.Services.Interfaces;

namespace EcoNotifications.Backend.DataAccess.Domain.Services;

public class SendEmail : ISendEmail
{
    public Task SendEmailAsync(string emailTo, string subject, string message)
    {
        // отправитель - устанавливаем адрес и отображаемое в письме имя
        var from = new MailAddress("trintsa@mail.ru", "econotofocation");
        // кому отправляем
        var to = new MailAddress(emailTo);
        // создаем объект сообщения
        var m = new MailMessage(from, to);
        // тема письма
        m.Subject = subject;
        // текст письма
        m.Body = message;
        // адрес smtp-сервера и порт, с которого будем отправлять письмо
        var smtp = new SmtpClient("smtp.mail.ru", 587);
        // логин и пароль
        smtp.Credentials = new NetworkCredential("trintsa@mail.ru", "pZHkdrWTiZ5cdHEjmYvQ");
        smtp.EnableSsl = true;
        smtp.Send(m);
        return Task.CompletedTask;
    }
}