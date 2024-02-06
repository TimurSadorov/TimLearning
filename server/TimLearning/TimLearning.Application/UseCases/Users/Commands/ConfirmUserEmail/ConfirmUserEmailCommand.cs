using MediatR;
using TimLearning.Application.UseCases.Users.Dto;

namespace TimLearning.Application.UseCases.Users.Commands.ConfirmUserEmail;

public record ConfirmUserEmailCommand(UserEmailConfirmationDto EmailConfirmationDto) : IRequest;
