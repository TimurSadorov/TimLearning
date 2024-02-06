using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using TimLearning.Infrastructure.Implementation.Configurations.Options;
using TimLearning.Infrastructure.Interfaces.Services.Email;

namespace TimLearning.Infrastructure.Implementation.Services.Email;

public class EmailService : IEmailService
{
    private readonly MailOptions _mailOptions;

    public EmailService(IOptions<MailOptions> mailOptions)
    {
        _mailOptions = mailOptions.Value;
    }

    public async Task SendMailAsync(MailDto mailDto)
    {
        var message = CreateMessage(mailDto);

        using var client = new SmtpClient();
        // client.ServerCertificateValidationCallback = (s, c, h, e) => true;
        await client.ConnectAsync(_mailOptions.Server, _mailOptions.Port, _mailOptions.UseSsl);
        // client.AuthenticationMechanisms.Remove("XOAUTH2");
        await client.AuthenticateAsync(_mailOptions.UserMail, _mailOptions.Password);
        await client.SendAsync(message);
        await client.DisconnectAsync(true);
    }

    private MimeMessage CreateMessage(MailDto mailDto)
    {
        var message = new MimeMessage
        {
            From = { new MailboxAddress(_mailOptions.UserName, _mailOptions.UserMail) },
            Subject = mailDto.Subject,
            Body = new BodyBuilder
            {
                HtmlBody = mailDto.HtmlBody,
                TextBody = mailDto.TextBody
            }.ToMessageBody(),
            Date = mailDto.CreateAt
        };
        mailDto.To.ForEach(to => message.To.Add(MailboxAddress.Parse(to)));

        return message;
    }
}
