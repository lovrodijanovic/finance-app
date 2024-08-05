using FinanceApp.Server.Models.Definitions;

namespace FinanceApp.Server.Models.DTO;

public class FinancialForm
{
    public Guid UserId { get; set; }

    public bool HasBudget { get; set; }

    public bool HasEmergencyFund { get; set; }

    public EmergencyFund? EmergencyFund { get; set; }

    public bool HasDebt { get; set; }

    public IEnumerable<Debt>? Debts { get; set; }

    public bool HasVoluntaryPensionInsurance { get; set; }

    public VoluntaryPensionInsurance? VoluntaryPensionInsurance { get; set; }

    public bool HasInvestments { get; set; }

    public IEnumerable<Investment>? Investments { get; set; }

    public int RiskSensitivity { get; set; }

    public decimal NetEarnings { get; set; }
}
