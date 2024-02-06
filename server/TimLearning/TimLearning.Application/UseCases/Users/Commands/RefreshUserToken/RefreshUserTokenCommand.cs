using MediatR;
using TimLearning.Application.Services.UserServices.Dto;
using TimLearning.Application.UseCases.Users.Dto;

namespace TimLearning.Application.UseCases.Users.Commands.RefreshUserToken;

public record RefreshUserTokenCommand(RefreshableTokenDto TokenInfo) : IRequest<AuthTokensDto>;
