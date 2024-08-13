using FinanceApp.Server.Models.Definitions;

namespace FinanceApp.Server.Models.DTO;

public class DebtDto
{
    public virtual Guid DebtType { get; set; }

    public decimal Principal { get; set; }

    public decimal InterestRate { get; set; }

    public int MaturityYears { get; set; }

    public decimal MonthlyContribution { get; set; }
}
