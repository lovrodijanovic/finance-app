using FinanceApp.Server.Data;
using FinanceApp.Server.Models.Definitions;
using FinanceApp.Server.Models.DTO;
using Mapster;

namespace FinanceApp.Server.Services;

public class FormService : BaseService
{
    public FormService(DataContext ctx) : base (ctx)
    {
        
    }

    public async Task<FinancialForm> SubmitForm(FinancialForm model)
    {
        var financialStatus = new FinancialStatus()
        {
            Id = Guid.NewGuid(),
            DateCreated = DateTime.Now,
            DateUpdated = DateTime.Now,
            HasBudget = model.HasBudget,
            HasDebt = model.HasDebt,
            HasInvestments = model.HasInvestments,
            HasEmergencyFund = model.HasEmergencyFund,
            HasVoluntaryPensionInsurance = model.HasVoluntaryPensionInsurance,
            NetEarnings = model.NetEarnings,
            RiskSensitivity = model.RiskSensitivity,
            FinancialScore = CalculateFinancialScore(model)
        };

        await _ctx.FinancialStatus.AddAsync(financialStatus);

        if (model.Investments != null)
        {
            var investments = new List<Investment>();
            foreach (var investment in model.Investments)
            {
                investments.Add(new Investment()
                {
                    DateCreated = DateTime.Now,
                    DateUpdated = DateTime.Now,
                    FinancialStatusId = financialStatus.Id,
                    Amount = investment.Amount,
                    MonthlyContribution = investment.MonthlyContribution,
                });  
            }
            await _ctx.Investment.AddRangeAsync(investments);
        }

        if (model.Debts != null)
        {
            var debts = new List<Debt>();
            foreach (var debt in model.Debts)
            {
                debts.Add(new Debt()
                {
                    DateCreated = DateTime.Now,
                    DateUpdated = DateTime.Now,
                    FinancialStatusId = financialStatus.Id,
                    Principal = debt.Principal,
                    DebtType = debt.DebtType,
                    InterestRate = debt.InterestRate,
                });
            }
            await _ctx.Debt.AddRangeAsync(debts);
        }

        await _ctx.SaveChangesAsync();
        
        return financialStatus.Adapt<FinancialForm>();
    }

    private static int CalculateFinancialScore(FinancialForm model)
    {
        int score = 10;
        const decimal minimalVoluntaryPensionInsuranceForFullStimulus = 53.31m;
        const decimal smallEmergencyFundAmount = 1000;
        decimal fullyFundedEmergencyFund = model.NetEarnings * 3;

        if (!model.HasBudget)
        {
            score -= 2;
        }

        if (!model.HasEmergencyFund || model.EmergencyFund!.Amount < smallEmergencyFundAmount)
        {
            score -= 2;
        }
        else if (model.HasEmergencyFund && model.EmergencyFund!.Amount < fullyFundedEmergencyFund)
        {
            score -= 1;
        }

        if (model.HasDebt)
        {
            score -= model.Debts!.Count();
        }

        if (!model.HasVoluntaryPensionInsurance)
        {
            score -= 1;
        }
        else if (model.HasVoluntaryPensionInsurance && model.EmergencyFund!.Amount < minimalVoluntaryPensionInsuranceForFullStimulus)
        {
            score -= 1;
        }

        if (!model.HasInvestments)
        {
            score -= 1;
        }

        return score;
    }
}
