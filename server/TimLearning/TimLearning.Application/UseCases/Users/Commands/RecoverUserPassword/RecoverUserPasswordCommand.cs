using MediatR;
using TimLearning.Application.UseCases.Users.Dto;

namespace TimLearning.Application.UseCases.Users.Commands.RecoverUserPassword;

public record RecoverUserPasswordCommand(NewRecoveringPasswordDto Dto) : IRequest;
