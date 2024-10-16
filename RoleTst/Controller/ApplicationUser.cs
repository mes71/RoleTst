using Microsoft.AspNetCore.Identity;

namespace RoleTst.Controller;

public class ApplicationUser : IdentityUser
{
    public string OTPCode { get; set; }
    public DateTime OTPIssueTime { get; set; }
}