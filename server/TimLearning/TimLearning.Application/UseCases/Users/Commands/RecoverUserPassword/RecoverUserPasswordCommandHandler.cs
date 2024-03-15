using MediatR;
using Microsoft.EntityFrameworkCore;
using TimLearning.Application.UseCases.Users.Dto;
using TimLearning.Domain.Services.UserServices;
using TimLearning.Infrastructure.Interfaces.Db;
using TimLearning.Shared.Validation.FluentValidator.Validators;

namespace TimLearning.Application.UseCases.Users.Commands.RecoverUserPassword;

public class RecoverUserPasswordCommandHandler : IRequestHandler<RecoverUserPasswordCommand>
{
    private readonly ICombinedFluentAndSimpleValidator<NewRecoveringPasswordDto> _newRecoveringPasswordValidator;
    private readonly IUserPasswordService _userPasswordService;
    private readonly IAppDbContext _db;

    public RecoverUserPasswordCommandHandler(
        ICombinedFluentAndSimpleValidator<NewRecoveringPasswordDto> newRecoveringPasswordValidator,
        IUserPasswordService userPasswordService,
        IAppDbContext db
    )
    {
        _newRecoveringPasswordValidator = newRecoveringPasswordValidator;
        _userPasswordService = userPasswordService;
        _db = db;
    }

    public async Task Handle(
        RecoverUserPasswordCommand request,
        CancellationToken cancellationToken
    )
    {
        var newPasswordInfo = request.Dto;
        await _newRecoveringPasswordValidator.ValidateAndThrowAsync(
            newPasswordInfo,
            cancellationToken
        );

        var newPasswordWithSalt = _userPasswordService.GetPasswordHash(newPasswordInfo.NewPassword);

        await _db.Users
            .Where(u => u.Email == newPasswordInfo.UserEmail)
            .ExecuteUpdateAsync(
                setter =>
                    setter
                        .SetProperty(u => u.PasswordHash, newPasswordWithSalt.HashWithSalt)
                        .SetProperty(u => u.PasswordSalt, newPasswordWithSalt.Salt),
                cancellationToken
            );
    }
}
