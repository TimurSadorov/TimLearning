using MediatR;
using Microsoft.EntityFrameworkCore;
using TimLearning.Application.UseCases.Users.Dto;
using TimLearning.Infrastructure.Interfaces.Db;
using TimLearning.Shared.Validation.Validators;

namespace TimLearning.Application.UseCases.Users.Commands.ConfirmUserEmail;

public class ConfirmUserEmailCommandHandler : IRequestHandler<ConfirmUserEmailCommand>
{
    private readonly IAppDbContext _db;
    private readonly IAsyncSimpleValidator<UserEmailConfirmationDto> _userEmailConfirmationDtoValidator;

    public ConfirmUserEmailCommandHandler(
        IAppDbContext db,
        IAsyncSimpleValidator<UserEmailConfirmationDto> userEmailConfirmationDtoValidator
    )
    {
        _db = db;
        _userEmailConfirmationDtoValidator = userEmailConfirmationDtoValidator;
    }

    public async Task Handle(ConfirmUserEmailCommand request, CancellationToken cancellationToken)
    {
        var confirmation = request.EmailConfirmationDto;
        await _userEmailConfirmationDtoValidator.ValidateAndThrowAsync(
            confirmation,
            cancellationToken
        );

        await _db.Users
            .Where(u => u.Email == confirmation.Email)
            .ExecuteUpdateAsync(
                setter => setter.SetProperty(user => user.IsEmailConfirmed, true),
                cancellationToken
            );
    }
}
