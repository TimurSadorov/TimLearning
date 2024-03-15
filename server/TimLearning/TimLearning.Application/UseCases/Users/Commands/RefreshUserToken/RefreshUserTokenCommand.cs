using MediatR;
using TimLearning.Application.UseCases.Users.Dto;
using TimLearning.Domain.Services.UserServices.Dto;

namespace TimLearning.Application.UseCases.Users.Commands.RefreshUserToken;

public record RefreshUserTokenCommand(RefreshableTokenDto TokenInfo) : IRequest<AuthTokensDto>;
