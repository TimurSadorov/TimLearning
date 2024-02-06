using TimLearning.Infrastructure.Interfaces.Services.Email;

namespace TimLearning.Application.Services.UserServices;

public interface IUserEmailProvider
{
    MailDto GetMailForEmailConfirmation(
        string userEmail,
        string linkToConfirmation,
        DateTimeOffset createAt
    );

    MailDto GetMailForPasswordRecovering(
        string userEmail,
        string linkToRecovering,
        DateTimeOffset createAt
    );
}
