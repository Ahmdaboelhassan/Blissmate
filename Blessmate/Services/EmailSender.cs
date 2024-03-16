using Blessmate.Helpers;
using Blessmate.Services.IServices;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Blessmate.Services;

public class EmailSender : IEmailSender
{
    private readonly EmailSetting _emailSetting;
    
    public EmailSender(IOptions<EmailSetting> emailSettingOptions)
    {
        _emailSetting = emailSettingOptions.Value;
    }
    public async Task SendEmail(string to, string subject, string body)
    {
        var email = new MimeMessage{
            Sender = MailboxAddress.Parse(_emailSetting.email),
            Subject = subject
        };

        var builder  = new BodyBuilder{
            HtmlBody = body
        };
        
        email.Body = builder.ToMessageBody();
        email.To.Add(MailboxAddress.Parse(to));
        email.From.Add(new MailboxAddress(_emailSetting.displayName,_emailSetting.email));

        using var smtp = new SmtpClient();
        smtp.Connect(_emailSetting.host,_emailSetting.port,SecureSocketOptions.StartTls);
        smtp.Authenticate(_emailSetting.email,_emailSetting.password);
        await smtp.SendAsync(email);

        smtp.Disconnect(true);
    }
}
