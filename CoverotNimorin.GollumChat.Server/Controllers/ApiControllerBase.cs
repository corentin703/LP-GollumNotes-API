using CoverotNimorin.GollumChat.Server.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CoverotNimorin.GollumChat.Server.Controllers;

[ApiController]
public abstract class ApiControllerBase : ControllerBase
{
    protected User? CurrentUser => HttpContext.Items["User"] as User;
}