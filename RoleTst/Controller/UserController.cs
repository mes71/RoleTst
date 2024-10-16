using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RoleTst.Controller;

[Authorize(Roles = "User")]
[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    [HttpGet("Dashboard")]
    public IActionResult GetUserDashboard()
    {
        return Ok("Welcome to User Dashboard");
    }
}