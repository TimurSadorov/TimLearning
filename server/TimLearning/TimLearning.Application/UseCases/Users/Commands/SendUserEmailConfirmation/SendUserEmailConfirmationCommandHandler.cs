using MediatR;
using Microsoft.EntityFrameworkCore;
using TimLearning.Application.Services.DataEncryptors.UserDataEncryptor;
using TimLearning.Domain.Entities;
using TimLearning.Infrastructure.Interfaces.Db;
using TimLearning.Infrastructure.Interfaces.Factories.Link;
using TimLearning.Infrastructure.Interfaces.Services.Email;
using TimLearning.Shared.Specifications.Dynamic;
using TimLearning.Shared.Validation.Exceptions.Localized;

namespace TimLearning.Application.UseCases.Users.Commands.SendUserEmailConfirmation;

public class SendUserEmailConfirmationCommandHandler
    : IRequestHandler<SendUserEmailConfirmationCommand>
{
    private readonly IAppDbContext _db;
    private readonly IEmailService _emailService;
    private readonly IUserDataEncryptor _userDataEncryptor;
    private readonly ITimLearningLinkFactory _timLearningLinkFactory;

    public SendUserEmailConfirmationCommandHandler(
        IAppDbContext db,
        IEmailService emailService,
        IUserDataEncryptor userDataEncryptor,
        ITimLearningLinkFactory timLearningLinkFactory
    )
    {
        _db = db;
        _emailService = emailService;
        _timLearningLinkFactory = timLearningLinkFactory;
        _userDataEncryptor = userDataEncryptor;
    }

    public async Task Handle(
        SendUserEmailConfirmationCommand request,
        CancellationToken cancellationToken
    )
    {
        var userId = request.NewConfirmationDto.UserId;
        var userEmail = await _db.Users
            .Where(new EntityByGuidIdSpecification<User>(userId))
            .Select(u => u.Email)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);
        if (userEmail is null)
        {
            LocalizedValidationException.ThrowWithSimpleTextError(
                $"Пользователь с индификатором[{userId}] не найден."
            );
        }

        var signedEmail = _userDataEncryptor.SingEmail(userEmail);
        var linkToConfirm = _timLearningLinkFactory.GetLinkToUserConfirm(userEmail, signedEmail);

        await _emailService.SendMailAsync(
            new MailDto(
                userEmail,
                "TimLearning. Подтверждение почты.",
                $"<p> Перейдите по <a href='{linkToConfirm}'>ссылке</a>, чтобы подтвердить почту.</p>",
                $"Перейдите по ссылке[{linkToConfirm}], чтобы подтвердить почту.",
                DateTimeOffset.UtcNow
            )
        );
    }
}
