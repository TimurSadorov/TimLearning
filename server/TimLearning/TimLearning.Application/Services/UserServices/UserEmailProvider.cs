using TimLearning.Infrastructure.Interfaces.Services.Email;

namespace TimLearning.Application.Services.UserServices;

public class UserEmailProvider : IUserEmailProvider
{
    public MailDto GetMailForEmailConfirmation(
        string userEmail,
        string linkToConfirmation,
        DateTimeOffset createAt
    )
    {
        return new MailDto(
            userEmail,
            "TimLearning. Подтверждение почты.",
            $"<p> Перейдите по <a href='{linkToConfirmation}'>ссылке</a>, чтобы подтвердить почту.</p>",
            $"Перейдите по ссылке[{linkToConfirmation}], чтобы подтвердить почту.",
            createAt
        );
    }

    public MailDto GetMailForPasswordRecovering(
        string userEmail,
        string linkToRecovering,
        DateTimeOffset createAt
    )
    {
        return new MailDto(
            userEmail,
            "TimLearning. Восстановление пароля.",
            $"<p> Перейдите по <a href='{linkToRecovering}'>ссылке</a>, чтобы восстановить пароль.</p>",
            $"Перейдите по ссылке[{linkToRecovering}], чтобы восстановить пароль.",
            createAt
        );
    }
}
