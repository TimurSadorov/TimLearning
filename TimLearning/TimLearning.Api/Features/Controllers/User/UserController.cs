using Microsoft.AspNetCore.Mvc;
using TimLearning.Api.Requests.User;

namespace TimLearning.Api.Features.Controllers.User;

[Route("user")]
public class UserController : BaseController
{
    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> RegisterAsync(NewUserRequest request)
    {
        var tokens = await _authService.RegisterAsync(request);
        return Ok(tokens);
    }
}