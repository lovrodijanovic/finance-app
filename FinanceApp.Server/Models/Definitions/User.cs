using Microsoft.AspNetCore.Identity;

namespace FinanceApp.Server.Models.Definitions;

public class User : IdentityUser
{
    public DateTime DateOfBirth { get; set; }

    public int Age { get; set; }
}
