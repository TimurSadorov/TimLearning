using MediatR;
using TimLearning.Application.UseCases.Users.Dto;

namespace TimLearning.Application.UseCases.Users.Commands.SendUserPasswordRecovering;

public record SendUserPasswordRecoveringCommand(UserRecoveringPasswordDto Dto) : IRequest;
