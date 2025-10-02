using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Api.Controllers;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]/[action]")]
//api/v1/catalog/getAllProducts
[ApiController]
// [Authorize]
public class ApiController : ControllerBase;