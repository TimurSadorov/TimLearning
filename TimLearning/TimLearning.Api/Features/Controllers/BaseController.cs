using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;

namespace TimLearning.Api.Features.Controllers;

[ApiController]
[Produces(MediaTypeNames.Application.Json)]
public class BaseController : ControllerBase;