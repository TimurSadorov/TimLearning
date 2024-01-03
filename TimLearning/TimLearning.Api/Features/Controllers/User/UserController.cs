using Microsoft.AspNetCore.Mvc;
using TimLearning.Api.Requests.User;
using TimLearning.Model.Dto.User;
using TimLearning.Model.Services;

namespace TimLearning.Api.Features.Controllers.User;

[Route("user")]
public class UserController : BaseController
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    [Route("register")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> RegisterAsync(NewUserRequest request)
    {
        await _userService.RegisterAsync(new NewUserDto(request.Email, request.Password));
        return Created();
    }
}