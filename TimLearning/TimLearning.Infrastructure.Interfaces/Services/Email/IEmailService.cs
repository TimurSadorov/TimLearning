namespace TimLearning.Infrastructure.Interfaces.Services.Email;

public interface IEmailService
{
    Task SendMailAsync(MailDto mailDto);
}
