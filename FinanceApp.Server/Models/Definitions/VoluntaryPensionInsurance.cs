using System.ComponentModel.DataAnnotations.Schema;

namespace FinanceApp.Server.Models.Definitions;

public class VoluntaryPensionInsurance : BaseEntity
{
    [ForeignKey("FinancialStatusId")]
    public virtual FinancialStatus? FinancialStatus { get; set; }

    public decimal Amount { get; set; }

    public decimal MonthlyContribution { get; set; }
}
