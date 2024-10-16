using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace RoleTst.Controller;

[ApiController]
[Route("api/v1/[controller]/[action]")]
public class AccountController : ControllerBase
{
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;

    public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto model)
    {
        var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
        var result = await _userManager.CreateAsync(user, model.Password);

        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(user, "User");
            return Ok("User registered successfully");
        }

        return BadRequest(result.Errors);
    }

    [HttpGet]
    public string TestConnection()
    {
        return "API is reachable!";
    }

    [HttpPost("Loginss")]
    public Task<string> Login([FromBody] string model)
    {
        return Task.FromResult<string>("OTP sent to your email or phone.");
        /*var user = await _userManager.FindByEmailAsync(model.Email);
        if (user != null)
        {
            var result = await _signInManager.PasswordSignInAsync(user.UserName, model.Password, false, false);
            if (result.Succeeded)
            /*#1#{
                // Generate OTP
                user.OTPCode = await OTPHelper.GenerateOTP();
                user.OTPIssueTime = DateTime.Now;
                await _userManager.UpdateAsync(user);



                return Ok("OTP sent to your email or phone.");
            }
        }*/
    }

    [HttpPost("ValidateOTP")]
    public async Task<IActionResult> ValidateOTP([FromBody] OTPDto model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user != null)
            if (OTPHelper.IsOTPValid(model.OTP, user.OTPCode, user.OTPIssueTime))
                return Ok("OTP is valid. Login successful.");

        return Unauthorized("Invalid OTP.");
    }
}