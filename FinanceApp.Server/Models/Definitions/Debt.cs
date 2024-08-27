using System.ComponentModel.DataAnnotations.Schema;

namespace FinanceApp.Server.Models.Definitions;

public class Debt : BaseEntity
{
    public Guid FinancialStatusId;

    [ForeignKey("FinancialStatusId")]
    public virtual FinancialStatus? FinancialStatus { get; set;}

    public string DebtName { get; set; } = "";

    public decimal RemainingBalance { get; set; }

    public decimal InterestRate { get; set; }

    public decimal MonthlyContribution { get; set; }
}
