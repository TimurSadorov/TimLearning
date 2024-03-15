using MediatR;
using Microsoft.EntityFrameworkCore;
using TimLearning.Application.Data.ValueObjects;
using TimLearning.Application.Services.UserServices;
using TimLearning.Infrastructure.Interfaces.Db;
using TimLearning.Infrastructure.Interfaces.Factories.Link;
using TimLearning.Infrastructure.Interfaces.Providers.Clock;
using TimLearning.Infrastructure.Interfaces.Services.Email;
using TimLearning.Shared.Validation.Validators;

namespace TimLearning.Application.UseCases.Users.Commands.SendUserPasswordRecovering;

public class SendUserPasswordRecoveringCommandHandler
    : IRequestHandler<SendUserPasswordRecoveringCommand>
{
    private readonly IAppDbContext _db;
    private readonly IEmailService _emailService;
    private readonly IUserDataEncryptor _userDataEncryptor;
    private readonly ITimLearningLinkFactory _timLearningLinkFactory;
    private readonly IUserEmailProvider _userEmailProvider;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IAsyncSimpleValidator<UserEmailValueObject> _userIdValidator;

    public SendUserPasswordRecoveringCommandHandler(
        IAppDbContext db,
        IEmailService emailService,
        IUserDataEncryptor userDataEncryptor,
        ITimLearningLinkFactory timLearningLinkFactory,
        IUserEmailProvider userEmailProvider,
        IDateTimeProvider dateTimeProvider,
        IAsyncSimpleValidator<UserEmailValueObject> userIdValidator
    )
    {
        _db = db;
        _emailService = emailService;
        _userDataEncryptor = userDataEncryptor;
        _timLearningLinkFactory = timLearningLinkFactory;
        _userEmailProvider = userEmailProvider;
        _dateTimeProvider = dateTimeProvider;
        _userIdValidator = userIdValidator;
    }

    public async Task Handle(
        SendUserPasswordRecoveringCommand request,
        CancellationToken cancellationToken
    )
    {
        var userEmail = request.Dto.UserEmail;
        await _userIdValidator.ValidateAndThrowAsync(
            new UserEmailValueObject(userEmail),
            cancellationToken
        );

        var userPasswordHash = await _db.Users
            .Where(u => u.Email == userEmail)
            .Select(u => u.PasswordHash)
            .SingleAsync(cancellationToken);

        var signedEmailAndPassword = _userDataEncryptor.SingEmailAndPassword(
            userEmail,
            userPasswordHash
        );
        var linkToRecovering = _timLearningLinkFactory.GetLinkToRecoverPassword(
            userEmail,
            signedEmailAndPassword
        );
        var mail = _userEmailProvider.GetMailForPasswordRecovering(
            userEmail,
            linkToRecovering,
            await _dateTimeProvider.GetUtcNow()
        );

        await _emailService.SendMailAsync(mail);
    }
}
