namespace Blessmate.Services.IServices;

public interface IEmailSender
{
    Task SendEmail(string to ,string subject ,string body);
}
