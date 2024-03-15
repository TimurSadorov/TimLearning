using MediatR;
using TimLearning.Application.UseCases.Users.Dto;
using TimLearning.Domain.Services.UserServices.Dto;

namespace TimLearning.Application.UseCases.Users.Commands.LoginUser;

public record LoginUserCommand(AuthorizationDto AuthorizationDto) : IRequest<AuthTokensDto>;
