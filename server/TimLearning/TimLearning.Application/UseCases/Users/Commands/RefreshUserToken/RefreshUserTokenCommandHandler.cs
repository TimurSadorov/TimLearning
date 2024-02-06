using MediatR;
using TimLearning.Application.Services.UserServices;
using TimLearning.Application.Services.UserServices.Dto;
using TimLearning.Application.UseCases.Users.Dto;
using TimLearning.Shared.Validation.Validators;

namespace TimLearning.Application.UseCases.Users.Commands.RefreshUserToken;

public class RefreshUserTokenCommandHandler
    : IRequestHandler<RefreshUserTokenCommand, AuthTokensDto>
{
    private readonly IAsyncSimpleValidator<RefreshableTokenDto> _refreshableTokenDtoValidator;
    private readonly IUserTokenUpdater _userTokenUpdater;

    public RefreshUserTokenCommandHandler(
        IAsyncSimpleValidator<RefreshableTokenDto> refreshableTokenDtoValidator,
        IUserTokenUpdater userTokenUpdater
    )
    {
        _refreshableTokenDtoValidator = refreshableTokenDtoValidator;
        _userTokenUpdater = userTokenUpdater;
    }

    public async Task<AuthTokensDto> Handle(
        RefreshUserTokenCommand request,
        CancellationToken cancellationToken
    )
    {
        var dto = request.TokenInfo;
        await _refreshableTokenDtoValidator.ValidateAndThrowAsync(dto, cancellationToken);

        var newTokens = await _userTokenUpdater.UpdateUserTokens(dto.UserEmail);

        return newTokens;
    }
}
