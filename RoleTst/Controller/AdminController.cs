using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RoleTst.Controller;

[Authorize(Roles = "Admin")]
[ApiController]
[Route("api/v1/[controller]")]
public class AdminController : ControllerBase
{
    [HttpGet("Dashboard")]
    public IActionResult GetAdminDashboard()
    {
        return Ok("Welcome to Admin Dashboard");
    }
}