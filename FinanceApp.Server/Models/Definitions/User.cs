using Microsoft.AspNetCore.Identity;

namespace FinanceApp.Server.Models.Definitions;

public class User : IdentityUser
{
    public int Age { get; set; }
}
