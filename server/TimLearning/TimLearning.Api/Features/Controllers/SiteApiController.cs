using System.Net.Mime;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using TimLearning.Api.Consts;
using TimLearning.Application.Auth.Claim;

namespace TimLearning.Api.Features.Controllers;

[ApiController]
[Produces(MediaTypeNames.Application.Json)]
[EnableCors(CorsNames.TimLearningSite)]
public abstract class SiteApiController : ControllerBase
{
    protected Guid UserId => User.GetUserId();
}
