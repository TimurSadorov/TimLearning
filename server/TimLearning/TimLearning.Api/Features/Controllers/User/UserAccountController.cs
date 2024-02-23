using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TimLearning.Api.Consts;
using TimLearning.Api.Mappers.User;
using TimLearning.Api.Requests.User;
using TimLearning.Api.Responses.User;
using TimLearning.Application.UseCases.Users.Commands.ConfirmUserEmail;
using TimLearning.Application.UseCases.Users.Commands.LoginUser;
using TimLearning.Application.UseCases.Users.Commands.RecoverUserPassword;
using TimLearning.Application.UseCases.Users.Commands.RefreshUserToken;
using TimLearning.Application.UseCases.Users.Commands.RegisterUser;
using TimLearning.Application.UseCases.Users.Commands.SendUserEmailConfirmation;
using TimLearning.Application.UseCases.Users.Commands.SendUserPasswordRecovering;
using TimLearning.Application.UseCases.Users.Dto;

namespace TimLearning.Api.Features.Controllers.User;

[Route($"{ApiRouteConsts.Prefix}/user/account")]
public class UserAccountController : SiteApiController
{
    private readonly IMediator _mediator;

    public UserAccountController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Route("register")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> Register([Required] NewUserRequest request)
    {
        await _mediator.Send(
            new RegisterUserCommand(new NewUserDto(request.Email, request.Password))
        );

        return Created();
    }

    [HttpPost]
    [Route("email/confirmation/send")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> SendEmailConfirmation(
        [Required] SendEmailConfirmationRequest request
    )
    {
        await _mediator.Send(
            new SendUserEmailConfirmationCommand(new NewUserEmailConfirmationDto(request.UserEmail))
        );

        return Ok();
    }

    [HttpPost]
    [Route("email/confirm")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> ConfirmEmail([Required] UserEmailConfirmationRequest request)
    {
        await _mediator.Send(
            new ConfirmUserEmailCommand(
                new UserEmailConfirmationDto(request.Email, request.Signature)
            )
        );

        return Ok();
    }

    [HttpPost]
    [Route("login")]
    public async Task<AuthTokensResponse> Login([Required] LoginRequest request)
    {
        var tokens = await _mediator.Send(
            new LoginUserCommand(new AuthorizationDto(request.Email, request.Password))
        );

        return tokens.ToResponse();
    }

    [HttpPost]
    [Route("refresh")]
    public async Task<AuthTokensResponse> Refresh([Required] RefreshRequest request)
    {
        var tokens = await _mediator.Send(
            new RefreshUserTokenCommand(
                new RefreshableTokenDto(request.RefreshToken, request.UserEmail)
            )
        );

        return tokens.ToResponse();
    }

    [HttpPost]
    [Route("password/mail/recover")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> SendMailToRecoverPassword(
        [Required] SendMailToRecoverPasswordRequest request
    )
    {
        await _mediator.Send(
            new SendUserPasswordRecoveringCommand(
                new UserInfoForRecoveringPasswordDto(request.UserEmail)
            )
        );

        return Ok();
    }

    [HttpPost]
    [Route("password/recover")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> RecoverPassword([Required] RecoverPasswordRequest request)
    {
        await _mediator.Send(
            new RecoverUserPasswordCommand(
                new NewRecoveringPasswordDto(
                    request.UserEmail,
                    request.Signature,
                    request.NewPassword
                )
            )
        );

        return Ok();
    }
}
