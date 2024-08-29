namespace FinanceApp.Server.Models.DTO;

public class FinancialStatusHistoryDto
{
    public DateTime DateCreated { get; set; }

    public int FinancialScore { get; set; }

    public int RiskSensitivity { get; set; }

    public decimal NetEarnings { get; set; }

    public int Age { get; set; }
}
