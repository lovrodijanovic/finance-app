namespace FinanceApp.Server.Models.DTO;

public class FormResultsDto
{
    public int FinancialScore { get; set; }

    public bool HasBudget { get; set; }

    public bool HasDebt { get; set; }

    public bool HasDebtsWithHighInterestRate { get; set; }

    public bool HasDebtsWithMediumInterestRate { get; set; }

    public bool HasDebtsWithLowInterestRate { get; set; }

    public bool HasEmergencyFund { get; set; }

    public bool HasSmallEmergencyFund { get; set; }

    public bool HasFullEmergencyFund { get; set; }

    public bool HasVoluntaryPensionInsurance { get; set; }

    public bool IsFullVoluntaryPensionInsuranceContribution { get; set; }

    public bool HasInvestments { get; set; }
}
