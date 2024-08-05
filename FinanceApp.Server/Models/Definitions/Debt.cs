using System.ComponentModel.DataAnnotations.Schema;

namespace FinanceApp.Server.Models.Definitions;

public class Debt : BaseEntity
{
    public Guid FinancialStatusId;

    [ForeignKey("FinancialStatusId")]
    public virtual FinancialStatus? FinancialStatus { get; set;}

    public Guid DebtTypeId;

    [ForeignKey("DebtTypeId")]
    public virtual DebtType? DebtType { get; set; }

    public decimal Principal { get; set; }

    public decimal InterestRate { get; set; }

    public int MaturityYears { get; set; }

    public decimal MonthlyContribution { get; set; }
}
