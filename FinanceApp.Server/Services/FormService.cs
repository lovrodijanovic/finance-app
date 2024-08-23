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

    const decimal minimalVoluntaryPensionInsuranceForFullStimulus = 55.36m;
    const decimal smallEmergencyFundAmount = 1000;

    public async Task<Guid> SubmitForm(FinancialFormDto model)
    {
        var financialStatus = new FinancialStatus()
        {
            Id = Guid.NewGuid(),
            DateCreated = DateTime.UtcNow,
            DateUpdated = DateTime.UtcNow,
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
                    Id = Guid.NewGuid(),
                    DateCreated = DateTime.UtcNow,
                    DateUpdated = DateTime.UtcNow,
                    FinancialStatusId = financialStatus.Id,
                    Amount = investment.Amount,
                    MonthlyContribution = investment.MonthlyContribution,
                    InvestmentTypeId = investment.InvestmentType
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
                    Id = Guid.NewGuid(),
                    DateCreated = DateTime.UtcNow,
                    DateUpdated = DateTime.UtcNow,
                    FinancialStatusId = financialStatus.Id,
                    Principal = debt.Principal,
                    MaturityYears = debt.MaturityYears,
                    MonthlyContribution = debt.MonthlyContribution,
                    DebtTypeId = debt.DebtType,
                    InterestRate = debt.InterestRate,
                });
            }
            await _ctx.Debt.AddRangeAsync(debts);
        }

        if (model.EmergencyFund != null)
        {
            var emergencyFund = new EmergencyFund()
            {
                Id = Guid.NewGuid(),
                DateCreated = DateTime.UtcNow,
                DateUpdated = DateTime.UtcNow,
                FinancialStatusId = financialStatus.Id,
                Amount = model.EmergencyFund.Amount,
                MonthlyContribution = model.EmergencyFund.MonthlyContribution
            };
            await _ctx.EmergencyFund.AddAsync(emergencyFund);
        }

        if (model.VoluntaryPensionInsurance != null)
        {
            var voluntaryPensionInsurance = new VoluntaryPensionInsurance()
            {
                Id = Guid.NewGuid(),
                DateCreated = DateTime.UtcNow,
                DateUpdated = DateTime.UtcNow,
                FinancialStatusId = financialStatus.Id,
                Amount = model.VoluntaryPensionInsurance.Amount,
                MonthlyContribution = model.VoluntaryPensionInsurance.MonthlyContribution
            };
            await _ctx.VoluntaryPensionInsurance.AddAsync(voluntaryPensionInsurance);
        }

        await _ctx.SaveChangesAsync();

        return financialStatus.Id;
    }

    public async Task<FormResultsDto> GetFormResults(Guid financialStatusId)
    {
        var financialStatus = _ctx.FinancialStatus.FirstOrDefault(x => x.Id == financialStatusId);
        var debts = _ctx.Debt.Where(x => x.FinancialStatusId == financialStatusId); 
        var investments = _ctx.Investment.Where(x => x.FinancialStatusId == financialStatusId);
        var emergencyFund = _ctx.EmergencyFund.FirstOrDefault(x => x.FinancialStatusId == financialStatusId);
        var voluntaryPensionInsurance = _ctx.VoluntaryPensionInsurance.FirstOrDefault(x => x.FinancialStatusId == financialStatusId);

        bool hasFullEmergencyFund = false;
        bool hasSmallEmergencyFund = false;

        if (emergencyFund != null)
        {
            hasFullEmergencyFund = emergencyFund.Amount > 3 * financialStatus.NetEarnings;
            hasSmallEmergencyFund = emergencyFund.Amount >= smallEmergencyFundAmount;
        }

        bool hasDebtsWithLowInterestRate = false;
        bool hasDebtsWithMediumInterestRate = false;
        bool hasDebtsWithHighInterestRate = false;
        if (debts != null)
        {
            hasDebtsWithLowInterestRate = debts.Any(x => x.InterestRate < 6);
            hasDebtsWithMediumInterestRate = debts.Any(x => x.InterestRate >= 6 && x.InterestRate < 8);
            hasDebtsWithHighInterestRate = debts.Any(x => x.InterestRate > 8);
        }

        var isFullVoluntaryPensionInsuranceContribution = false;
        if (voluntaryPensionInsurance != null)
        {
            isFullVoluntaryPensionInsuranceContribution = voluntaryPensionInsurance.MonthlyContribution >= minimalVoluntaryPensionInsuranceForFullStimulus;
        }

        var formResults = new FormResultsDto
        {
            FinancialScore = financialStatus.FinancialScore,
            HasBudget = financialStatus.HasBudget,
            HasEmergencyFund = financialStatus.HasEmergencyFund,
            HasSmallEmergencyFund = hasSmallEmergencyFund,
            HasFullEmergencyFund = hasFullEmergencyFund,
            HasDebt = financialStatus.HasDebt,
            HasDebtsWithLowInterestRate = hasDebtsWithLowInterestRate,
            HasDebtsWithMediumInterestRate = hasDebtsWithMediumInterestRate,
            HasDebtsWithHighInterestRate = hasDebtsWithHighInterestRate,
            HasVoluntaryPensionInsurance = financialStatus.HasVoluntaryPensionInsurance,
            IsFullVoluntaryPensionInsuranceContribution = isFullVoluntaryPensionInsuranceContribution,
            HasInvestments = financialStatus.HasInvestments
        };

        return formResults;
    }

    private static int CalculateFinancialScore(FinancialFormDto model)
    {
        int score = 10;
        decimal fullyFundedEmergencyFund = model.NetEarnings * 3;
        const decimal mediumInterestRate = 6;
        const decimal highInterestRate = 8;

        if (!model.HasBudget)
        {
            score -= 2;
        }

        if (!model.HasEmergencyFund)
        {
            score -= 2;
        }
        else
        {
            if (model.EmergencyFund.Amount < smallEmergencyFundAmount)
            {
                score -= 2;
            }
            else if (model.EmergencyFund.Amount < fullyFundedEmergencyFund)
            {
                score -= 1;
            }
        }

        if (model.HasDebt && model.Debts != null)
        {
            var isHighInterestRate = model.Debts.Any(x => x.InterestRate >= highInterestRate);
            if (model.Debts.Any(x => x.InterestRate >= highInterestRate))
            {
                foreach (var debt in model.Debts.Where(x => x.InterestRate >= highInterestRate))
                {
                    score -= 2;
                }
            }
            else if (model.Debts.Any(x => x.InterestRate >= mediumInterestRate && x.InterestRate < highInterestRate))
            {
                foreach (var debt in model.Debts.Where(x => x.InterestRate >= mediumInterestRate && x.InterestRate < highInterestRate))
                {
                    score -= 1;
                }
            }
        }

        if (!model.HasVoluntaryPensionInsurance)
        {
            score -= 2;
        }
        else if (model.HasVoluntaryPensionInsurance && model.VoluntaryPensionInsurance!.Amount < minimalVoluntaryPensionInsuranceForFullStimulus)
        {
            score -= 1;
        }

        if (!model.HasInvestments)
        {
            score -= 2;
        }

        return score >= 0 ? score : 0;
    }
}
