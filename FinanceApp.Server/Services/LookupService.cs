using FinanceApp.Server.Data;
using FinanceApp.Server.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace FinanceApp.Server.Services;

public class LookupService : BaseService
{
    public LookupService(DataContext ctx) : base(ctx)
    {
    }

    public async Task<IEnumerable<InvestmentTypeDto>> GetInvestmentTypes()
    {
        return await _ctx.InvestmentType.Select(x => new InvestmentTypeDto()
        {
            Id = x.Id,
            Name = x.Name,
            Description = x.Description,
            Abbreviation = x.Abbreviation,
            CroatianDescription = x.CroatianDescription,
            CroatianName = x.CroatianName,
        }).ToListAsync();
    }
}
