using MediatR;
using TimLearning.Application.Services.UserServices;
using TimLearning.Application.UseCases.Users.Dto;
using TimLearning.Domain.Services.UserServices.Dto;
using TimLearning.Shared.Validation.Validators;

namespace TimLearning.Application.UseCases.Users.Commands.LoginUser;

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, AuthTokensDto>
{
    private readonly IAsyncSimpleValidator<AuthorizationDto> _authorizationDtoValidator;
    private readonly IUserTokenUpdater _userTokenUpdater;

    public LoginUserCommandHandler(
        IAsyncSimpleValidator<AuthorizationDto> authorizationDtoValidator,
        IUserTokenUpdater userTokenUpdater
    )
    {
        _authorizationDtoValidator = authorizationDtoValidator;
        _userTokenUpdater = userTokenUpdater;
    }

    public async Task<AuthTokensDto> Handle(
        LoginUserCommand request,
        CancellationToken cancellationToken
    )
    {
        var dto = request.AuthorizationDto;
        await _authorizationDtoValidator.ValidateAndThrowAsync(dto, cancellationToken);

        var newTokens = await _userTokenUpdater.UpdateUserTokens(dto.Email);

        return newTokens;
    }
}
