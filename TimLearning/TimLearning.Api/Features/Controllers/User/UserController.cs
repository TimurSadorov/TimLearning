using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TimLearning.Api.Requests.User;
using TimLearning.Application.UseCases.Users.Commands.ConfirmUserEmail;
using TimLearning.Application.UseCases.Users.Commands.RegisterUser;
using TimLearning.Application.UseCases.Users.Dto;

namespace TimLearning.Api.Features.Controllers.User;

[Route("user")]
public class UserController : BaseController
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Route("register")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> Register(NewUserRequest request)
    {
        await _mediator.Send(
            new RegisterUserCommand(new NewUserDto(request.Email, request.Password))
        );

        return Created();
    }

    [HttpPost]
    [Route("email/confirm")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> ConfirmEmail(UserEmailConfirmationRequest request)
    {
        await _mediator.Send(
            new ConfirmUserEmailCommand(
                new UserEmailConfirmationDto(request.Email, request.Signature)
            )
        );

        return Ok();
    }
}
