using System.ComponentModel.DataAnnotations.Schema;

namespace FinanceApp.Server.Models.Definitions;

public class VoluntaryPensionInsurance : BaseEntity
{
    public Guid FinancialStatusId { get; set; }

    [ForeignKey("FinancialStatusId")]
    public virtual FinancialStatus? FinancialStatus { get; set; }

    public decimal MonthlyContribution { get; set; }
}
