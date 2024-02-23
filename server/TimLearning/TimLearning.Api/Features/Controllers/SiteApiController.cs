using System.Net.Mime;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using TimLearning.Api.Consts;

namespace TimLearning.Api.Features.Controllers;

[ApiController]
[Produces(MediaTypeNames.Application.Json)]
[EnableCors(CorsNamesConsts.TimLearningSite)]
public abstract class SiteApiController : ControllerBase;
