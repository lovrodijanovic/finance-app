using System.ComponentModel.DataAnnotations.Schema;

namespace FinanceApp.Server.Models.Definitions;

public class Investment : BaseEntity
{
    public Guid FinancialStatusId;

    [ForeignKey("FinancialStatusId")]
    public virtual FinancialStatus? FinancialStatus { get; set; }

    public Guid InvestmentTypeId;

    [ForeignKey("InvestmentTypeId")]
    public virtual InvestmentType? InvestmentType { get; set; }

    public decimal Amount { get; set; }

    public decimal MonthlyContribution { get; set; }
}
