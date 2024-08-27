namespace FinanceApp.Server.Models.DTO;

public class DebtDto
{
    public string DebtName { get; set; } = "";

    public decimal InterestRate { get; set; }

    public decimal RemainingBalance { get; set; }

    public decimal StartingBalance { get; set; }

    public decimal MonthlyContribution { get; set; }

    public decimal TotalInterest { get; set; }
}
