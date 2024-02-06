using MediatR;
using TimLearning.Application.UseCases.Users.Dto;

namespace TimLearning.Application.UseCases.Users.Commands.RegisterUser;

public record RegisterUserCommand(NewUserDto NewUserDto) : IRequest<Guid>;