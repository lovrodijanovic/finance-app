using FinanceApp.Server.Data;
using FinanceApp.Server.Models.Definitions;
using FinanceApp.Server.Models.DTO;
using Mapster;
using Microsoft.EntityFrameworkCore;

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
        var financialStatus = new FinancialStatus
        {
            Id = Guid.NewGuid(),
            DateCreated = DateTime.UtcNow,
            DateUpdated = DateTime.UtcNow,
            UserId = model.UserId,
            HasBudget = model.HasBudget,
            HasDebt = model.HasDebt,
            HasInvestments = model.HasInvestments,
            HasEmergencyFund = model.HasEmergencyFund,
            HasVoluntaryPensionInsurance = model.HasVoluntaryPensionInsurance,
            NetEarnings = model.NetEarnings,
            RiskSensitivity = model.RiskSensitivity,
            Age = model.Age,
            FinancialScore = CalculateFinancialScore(model)
        };

        await _ctx.FinancialStatus.AddAsync(financialStatus);

        if (model.Investments != null && model.Investments.Any())
        {
            await AddInvestments(model.Investments, financialStatus.Id);
        }
        
        if (model.Debts != null && model.Debts.Any())
        {
            await AddDebts(model.Debts, financialStatus.Id);
        }

        if (model.EmergencyFund != null)
        {
            await AddEmergencyFund(model.EmergencyFund, financialStatus.Id);
        }

        if (model.VoluntaryPensionInsurance != null)
        {
            await AddVoluntaryPensionInsurance(model.VoluntaryPensionInsurance, financialStatus.Id);
        }

        await _ctx.SaveChangesAsync();

        return financialStatus.Id;
    }

    private async Task AddInvestments(IEnumerable<InvestmentDto> investments, Guid financialStatusId)
    {
        var investmentEntities = investments.Select(investment => new Investment
        {
            Id = Guid.NewGuid(),
            DateCreated = DateTime.UtcNow,
            DateUpdated = DateTime.UtcNow,
            FinancialStatusId = financialStatusId,
            MonthlyContribution = investment.MonthlyContribution,
            InvestmentTypeId = investment.InvestmentType
        }).ToList();

        await _ctx.Investment.AddRangeAsync(investmentEntities);
    }

    private async Task AddDebts(IEnumerable<DebtDto> debts, Guid financialStatusId)
    {
        var debtEntities = debts.Select(debt => new Debt
        {
            Id = Guid.NewGuid(),
            DateCreated = DateTime.UtcNow,
            DateUpdated = DateTime.UtcNow,
            FinancialStatusId = financialStatusId,
            DebtName = debt.DebtName,
            RemainingBalance = debt.RemainingBalance,
            MonthlyContribution = debt.MonthlyContribution,
            InterestRate = debt.InterestRate,
        }).ToList();

        await _ctx.Debt.AddRangeAsync(debtEntities);
    }

    private async Task AddEmergencyFund(EmergencyFundDto emergencyFund, Guid financialStatusId)
    {
        var emergencyFundEntity = new EmergencyFund
        {
            Id = Guid.NewGuid(),
            DateCreated = DateTime.UtcNow,
            DateUpdated = DateTime.UtcNow,
            FinancialStatusId = financialStatusId,
            Amount = emergencyFund.Amount,
            MonthlyContribution = emergencyFund.MonthlyContribution
        };

        await _ctx.EmergencyFund.AddAsync(emergencyFundEntity);
    }

    private async Task AddVoluntaryPensionInsurance(VoluntaryPensionInsuranceDto voluntaryPensionInsurance, Guid financialStatusId)
    {
        var voluntaryPensionInsuranceEntity = new VoluntaryPensionInsurance
        {
            Id = Guid.NewGuid(),
            DateCreated = DateTime.UtcNow,
            DateUpdated = DateTime.UtcNow,
            FinancialStatusId = financialStatusId,
            MonthlyContribution = voluntaryPensionInsurance.MonthlyContribution
        };

        await _ctx.VoluntaryPensionInsurance.AddAsync(voluntaryPensionInsuranceEntity);
    }


    public async Task<FormResultsDto> GetFormResults(Guid financialStatusId)
    {
        var financialStatus = await _ctx.FinancialStatus.FirstOrDefaultAsync(x => x.Id == financialStatusId);
        var debts = await _ctx.Debt.Where(x => x.FinancialStatusId == financialStatusId).ProjectToType<DebtDto>().ToListAsync();
        var emergencyFund = await _ctx.EmergencyFund.FirstOrDefaultAsync(x => x.FinancialStatusId == financialStatusId);
        var voluntaryPensionInsurance = await _ctx.VoluntaryPensionInsurance.FirstOrDefaultAsync(x => x.FinancialStatusId == financialStatusId);

        bool hasFullEmergencyFund = false;
        bool hasSmallEmergencyFund = false;
        if (financialStatus != null && emergencyFund != null)
        {
            hasFullEmergencyFund = emergencyFund.Amount > 3 * financialStatus.NetEarnings;
            hasSmallEmergencyFund = emergencyFund.Amount >= smallEmergencyFundAmount;
        }

        bool hasDebtsWithLowInterestRate = false;
        bool hasDebtsWithMediumInterestRate = false;
        bool hasDebtsWithHighInterestRate = false;
        List<DebtPayoff> lowInterestRateDebtPayoffs = [];
        List<DebtPayoff> mediumInterestRateDebtPayoffs = [];
        List<DebtPayoff> highInterestRateDebtPayoffs = [];
        if (debts != null && debts.Any())
        {
            hasDebtsWithLowInterestRate = debts.Any(x => x.InterestRate < 6);
            hasDebtsWithMediumInterestRate = debts.Any(x => x.InterestRate >= 6 && x.InterestRate < 8);
            hasDebtsWithHighInterestRate = debts.Any(x => x.InterestRate >= 8);
            if (hasDebtsWithLowInterestRate)
            {
                lowInterestRateDebtPayoffs = CalculateDebtPayoffs(debts.Where(x => x.InterestRate < 6)).OrderByDescending(x => x.InterestRate).ToList();
            }
            if (hasDebtsWithMediumInterestRate)
            {
                mediumInterestRateDebtPayoffs = CalculateDebtPayoffs(debts.Where(x => x.InterestRate >= 6 && x.InterestRate < 8)).OrderByDescending(x => x.InterestRate).ToList();
            }
            if (hasDebtsWithHighInterestRate)
            {
                highInterestRateDebtPayoffs = CalculateDebtPayoffs(debts.Where(x => x.InterestRate >= 8)).OrderByDescending(x => x.InterestRate).ToList();
            }
        }

        var isFullVoluntaryPensionInsuranceContribution = false;
        if (voluntaryPensionInsurance != null)
        {
            isFullVoluntaryPensionInsuranceContribution = voluntaryPensionInsurance.MonthlyContribution >= minimalVoluntaryPensionInsuranceForFullStimulus;
        }

        decimal investmentAmountSuggestion = 0;
        decimal equityPercentageSuggestion = 0;
        bool hasLowInvestments = false;
        if (financialStatus != null)
        {
            var voluntaryPensionInsuranceMonthlyContribution = voluntaryPensionInsurance != null ? voluntaryPensionInsurance.MonthlyContribution : 0;
            investmentAmountSuggestion = financialStatus.NetEarnings * 0.2m - voluntaryPensionInsuranceMonthlyContribution;
            
            if (financialStatus.RiskSensitivity > 3)
            {
                equityPercentageSuggestion = 120 - financialStatus.Age;
            }
            else if (financialStatus.RiskSensitivity == 3)
            {
                equityPercentageSuggestion = 110 - financialStatus.Age;
            }
            else if (financialStatus.RiskSensitivity < 3)
            {
                equityPercentageSuggestion = 100 - financialStatus.Age;
            }

            if (financialStatus.HasInvestments && _ctx.Investment.Any(x => x.FinancialStatusId == financialStatusId))
            {
                var investments = _ctx.Investment.Where(x => x.FinancialStatusId == financialStatusId);
                var investmentSum = investments.Sum(x => x.MonthlyContribution);

                if (investmentSum < financialStatus.NetEarnings * 0.2m)
                {
                    hasLowInvestments = true;
                }
            }
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
            LowInterestRateDebtPayoffs = lowInterestRateDebtPayoffs,
            HasDebtsWithMediumInterestRate = hasDebtsWithMediumInterestRate,
            MediumInterestRateDebtPayoffs = mediumInterestRateDebtPayoffs,
            HasDebtsWithHighInterestRate = hasDebtsWithHighInterestRate,
            HighInterestRateDebtPayoffs = highInterestRateDebtPayoffs,
            HasVoluntaryPensionInsurance = financialStatus.HasVoluntaryPensionInsurance,
            IsFullVoluntaryPensionInsuranceContribution = isFullVoluntaryPensionInsuranceContribution,
            HasInvestments = financialStatus.HasInvestments,
            InvestmentAmountSuggestion = investmentAmountSuggestion,
            EquityPercentageSuggestion = equityPercentageSuggestion,
            HasLowInvestments = hasLowInvestments
        };

        return formResults;
    }

    public async Task<List<FinancialStatusHistoryDto>> GetFinancialStatusHistoryAsync(string userId)
    {
        var financialStatuses = await _ctx.FinancialStatus
            .Where(x => x.UserId == userId)
            .ToListAsync();

        var result = new List<FinancialStatusHistoryDto>();
        foreach (var financialStatus in financialStatuses)
        {
            result.Add(new FinancialStatusHistoryDto()
            {
                Id = financialStatus.Id,
                Age = financialStatus.Age,
                DateCreated = financialStatus.DateCreated,
                FinancialScore = financialStatus.FinancialScore,
                NetEarnings = financialStatus.NetEarnings,
                RiskSensitivity = financialStatus.RiskSensitivity
            });
        }

        return result;
    }

    private static List<DebtPayoff> CalculateDebtPayoffs(IEnumerable<DebtDto> debts)
    {
        var remainingDebts = debts.ToList();

        foreach (var debt in remainingDebts)
        {
            debt.StartingBalance = debt.RemainingBalance;
        }

        List<DebtPayoff> debtPayoffs = new();

        int month = 0;

        while (remainingDebts.Count > 0)
        {
            month++;

            foreach (var debt in remainingDebts.ToList())
            {
                decimal interestPayment = debt.RemainingBalance * (debt.InterestRate / 100 / 12);
                debt.RemainingBalance += interestPayment;

                if (debt.MonthlyContribution <= interestPayment)
                {
                    throw new UnpayableDebtException(debt.DebtName);
                }

                if (debt.RemainingBalance <= debt.MonthlyContribution)
                {
                    debt.RemainingBalance = 0;

                    debtPayoffs.Add(new DebtPayoff
                    {
                        DebtName = debt.DebtName,
                        NumberOfPayments = month,
                        InterestRate = debt.InterestRate,
                        TotalPayments = Math.Round(debt.StartingBalance + debt.TotalInterest + interestPayment, 2),
                        TotalInterest = Math.Round(debt.TotalInterest + interestPayment, 2)
                    });

                    var nextDebt = remainingDebts.OrderByDescending(d => d.InterestRate).FirstOrDefault();
                    if (nextDebt != null)
                    {
                        nextDebt.MonthlyContribution += debt.MonthlyContribution;
                    }

                    remainingDebts.Remove(debt);
                }
                else
                {
                    debt.RemainingBalance -= debt.MonthlyContribution;
                    debt.TotalInterest += interestPayment;
                }
            }
        }

        return debtPayoffs;
    }

    private static int CalculateFinancialScore(FinancialFormDto model)
    {
        int score = 10;
        decimal fullyFundedEmergencyFund = model.NetEarnings * 3;
        const int mediumInterestRate = 6;
        const int highInterestRate = 8;

        score -= model.HasBudget ? 0 : 2;

        if (!model.HasEmergencyFund 
            || (model.HasEmergencyFund && model.EmergencyFund != null && model.EmergencyFund.Amount < smallEmergencyFundAmount))
        {
            score -= 2;
        }
        else if (model.HasEmergencyFund && model.EmergencyFund != null && model.EmergencyFund.Amount < fullyFundedEmergencyFund)
        {
            score -= 1;
        }

        if (model.HasDebt && model.Debts != null)
        {
            score -= model.Debts.Count(x => x.InterestRate >= highInterestRate) * 2;
            score -= model.Debts.Count(x => x.InterestRate >= mediumInterestRate && x.InterestRate < highInterestRate);
        }

        if (!model.HasVoluntaryPensionInsurance)
        {
            score -= 2;
        }
        else if (model.HasVoluntaryPensionInsurance && model.VoluntaryPensionInsurance != null
            && model.VoluntaryPensionInsurance.MonthlyContribution < minimalVoluntaryPensionInsuranceForFullStimulus)
        {
            score -= 1;
        }

        score -= model.HasInvestments ? 0 : 2;

        return score >= 0 ? score : 0;
    }
}

public class UnpayableDebtException : Exception
{
    public string DebtName { get; }

    public UnpayableDebtException(string debtName)
        : base($"Dug '{debtName}' ne može biti plaćen jer je kamata svaki mjesec veća od mjesečne uplate.")
    {
        DebtName = debtName;
    }
}
