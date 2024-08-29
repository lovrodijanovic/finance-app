using FinanceApp.Server.Models.Definitions;

namespace FinanceApp.Server.Models.DTO;

public class InvestmentDto
{
    public virtual Guid InvestmentType { get; set; }

    public decimal MonthlyContribution { get; set; }
}
