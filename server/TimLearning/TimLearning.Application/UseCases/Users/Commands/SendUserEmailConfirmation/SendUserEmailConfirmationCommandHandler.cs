﻿using MediatR;
using TimLearning.Application.Data.ValueObjects;
using TimLearning.Application.Services.UserServices;
using TimLearning.Infrastructure.Interfaces.Factories.Link;
using TimLearning.Infrastructure.Interfaces.Providers.Clock;
using TimLearning.Infrastructure.Interfaces.Services.Email;
using TimLearning.Shared.Validation.Validators;

namespace TimLearning.Application.UseCases.Users.Commands.SendUserEmailConfirmation;

public class SendUserEmailConfirmationCommandHandler
    : IRequestHandler<SendUserEmailConfirmationCommand>
{
    private readonly IEmailService _emailService;
    private readonly IUserDataEncryptor _userDataEncryptor;
    private readonly ITimLearningLinkFactory _timLearningLinkFactory;
    private readonly IUserEmailProvider _userEmailProvider;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IAsyncSimpleValidator<UserEmailValueObject> _userIdValidator;

    public SendUserEmailConfirmationCommandHandler(
        IEmailService emailService,
        IUserDataEncryptor userDataEncryptor,
        ITimLearningLinkFactory timLearningLinkFactory,
        IUserEmailProvider userEmailProvider,
        IDateTimeProvider dateTimeProvider,
        IAsyncSimpleValidator<UserEmailValueObject> userIdValidator
    )
    {
        _emailService = emailService;
        _timLearningLinkFactory = timLearningLinkFactory;
        _userEmailProvider = userEmailProvider;
        _dateTimeProvider = dateTimeProvider;
        _userDataEncryptor = userDataEncryptor;
        _userIdValidator = userIdValidator;
    }

    public async Task Handle(
        SendUserEmailConfirmationCommand request,
        CancellationToken cancellationToken
    )
    {
        var userEmail = request.NewConfirmationDto.UserEmail;
        await _userIdValidator.ValidateAndThrowAsync(
            new UserEmailValueObject(userEmail),
            cancellationToken
        );

        var signedEmail = _userDataEncryptor.SingEmail(userEmail);
        var linkToConfirm = _timLearningLinkFactory.GetLinkToUserConfirm(userEmail, signedEmail);
        var mail = _userEmailProvider.GetMailForEmailConfirmation(
            userEmail,
            linkToConfirm,
            await _dateTimeProvider.GetUtcNow()
        );

        await _emailService.SendMailAsync(mail);
    }
}