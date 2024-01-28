using FluentValidation;
using MediatR;
using TimLearning.Application.Services.DataEncryptors.PasswordEncryptor;
using TimLearning.Application.UseCases.Users.Commands.SendUserEmailConfirmation;
using TimLearning.Application.UseCases.Users.Dto;
using TimLearning.Domain.Entities;
using TimLearning.Infrastructure.Interfaces.Db;
using TimLearning.Shared.Validation.FluentValidator.Extensions;

namespace TimLearning.Application.UseCases.Users.Commands.RegisterUser;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Guid>
{
    private readonly IAppDbContext _db;
    private readonly IValidator<NewUserDto> _newUserDtoValidator;
    private readonly IMediator _mediator;
    private readonly IPasswordEncryptor _passwordEncryptor;

    public RegisterUserCommandHandler(
        IAppDbContext db,
        IValidator<NewUserDto> newUserDtoValidator,
        IMediator mediator,
        IPasswordEncryptor passwordEncryptor
    )
    {
        _db = db;
        _newUserDtoValidator = newUserDtoValidator;
        _passwordEncryptor = passwordEncryptor;
        _mediator = mediator;
    }

    public async Task<Guid> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var newUserDto = request.NewUserDto;
        await _newUserDtoValidator.ValidateAndThrowLocalizedExceptionAsync(
            newUserDto,
            cancellationToken
        );

        var passwordWithSalt = _passwordEncryptor.GetHashedPassword(
            newUserDto.Password,
            saltSize: 16
        );
        var user = new User
        {
            Email = newUserDto.Email,
            IsEmailConfirmed = false,
            PasswordHash = passwordWithSalt.HashWithSalt,
            PasswordSalt = passwordWithSalt.Salt
        };

        _db.Add(user);

        await _db.SaveChangesAsync(cancellationToken);

        await _mediator.Send(
            new SendUserEmailConfirmationCommand(new NewUserEmailConfirmationDto(user.Id)),
            cancellationToken
        );

        return user.Id;
    }
}
