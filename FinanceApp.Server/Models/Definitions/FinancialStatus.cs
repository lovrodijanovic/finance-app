using System.ComponentModel.DataAnnotations.Schema;

namespace FinanceApp.Server.Models.Definitions;

public class FinancialStatus : BaseEntity
{
    public string UserId { get; set; } = "";

    [ForeignKey("UserId")]
    public virtual User? User { get; set; }

    public bool HasBudget { get; set; }

    public bool HasEmergencyFund { get; set; }

    public bool HasDebt { get; set; }

    public bool HasVoluntaryPensionInsurance { get; set; }

    public bool HasInvestments { get; set; }

    public int RiskSensitivity { get; set; }

    public decimal NetEarnings { get; set; }

    public int Age { get; set; }

    public int FinancialScore { get; set; }
}
