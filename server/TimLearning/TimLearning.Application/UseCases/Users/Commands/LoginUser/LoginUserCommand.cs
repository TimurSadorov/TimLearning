using MediatR;
using TimLearning.Application.Services.UserServices.Dto;
using TimLearning.Application.UseCases.Users.Dto;

namespace TimLearning.Application.UseCases.Users.Commands.LoginUser;

public record LoginUserCommand(AuthorizationDto AuthorizationDto) : IRequest<AuthTokensDto>;
