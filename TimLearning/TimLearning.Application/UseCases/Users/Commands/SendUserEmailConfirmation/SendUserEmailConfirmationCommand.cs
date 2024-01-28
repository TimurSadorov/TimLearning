using MediatR;
using TimLearning.Application.UseCases.Users.Dto;

namespace TimLearning.Application.UseCases.Users.Commands.SendUserEmailConfirmation;

public record SendUserEmailConfirmationCommand(NewUserEmailConfirmationDto NewConfirmationDto) : IRequest;
