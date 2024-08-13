using FinanceApp.Server.Models.Definitions;

namespace FinanceApp.Server.Models.DTO;

public class FinancialFormDto
{
    public Guid UserId { get; set; }

    public bool HasBudget { get; set; }

    public bool HasEmergencyFund { get; set; }

    public EmergencyFundDto? EmergencyFund { get; set; }

    public bool HasDebt { get; set; }

    public IEnumerable<DebtDto>? Debts { get; set; }

    public bool HasVoluntaryPensionInsurance { get; set; }

    public VoluntaryPensionInsuranceDto? VoluntaryPensionInsurance { get; set; }

    public bool HasInvestments { get; set; }

    public IEnumerable<InvestmentDto>? Investments { get; set; }

    public int RiskSensitivity { get; set; }

    public decimal NetEarnings { get; set; }
}
