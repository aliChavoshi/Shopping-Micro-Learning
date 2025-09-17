using Microsoft.AspNetCore.Mvc;

namespace Basket.Api.Controllers;

[Route("api/v{version:apiVersion}/[controller]/[action]")]
[ApiController]
public class ApiController : ControllerBase;