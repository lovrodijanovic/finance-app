namespace FinanceApp.Server.Models.Definitions;

public class User : BaseEntity
{
    public DateTime DateOfBirth { get; set; }

    public string Username { get; set; } = "";

    public string Password { get; set; } = "";

    public string Email { get; set; } = "";

    public int Age { get; set; }
}
