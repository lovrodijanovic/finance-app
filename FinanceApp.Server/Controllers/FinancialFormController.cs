using FinanceApp.Server.Models.DTO;
using FinanceApp.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace FinanceApp.Server.Controllers;
[Route("[controller]")]
[ApiController]
public class FinancialFormController : ControllerBase
{
    private readonly FormService _formService;
    private readonly LookupService _lookupService;

    public FinancialFormController(FormService formService, LookupService lookupService)
    {
        _formService = formService;
        _lookupService = lookupService;
    }

    [HttpPost("")]
    public async Task<ActionResult<Guid>> SubmitForm([FromBody] FinancialFormDto model)
    {
        var result = await _formService.SubmitForm(model);

        return Ok(new { FinancialStatusId = result} );
    }

    [HttpGet("{financialStatusId}")]
    public async Task<ActionResult<FormResultsDto>> GetResults([FromRoute] Guid financialStatusId)
    {
        var result = await _formService.GetFormResults(financialStatusId);
        return Ok(result);
    }

    [HttpGet("get-investment-types")]
    public async Task<ActionResult<IEnumerable<InvestmentTypeDto>>> GetInvestmentTypes()
    {
        var result = await _lookupService.GetInvestmentTypes();
        return Ok(result);
    }

    [HttpGet("get-financial-status-history/{userId}")]
    public async Task<ActionResult<IEnumerable<FinancialStatusHistoryDto>>> GetFinancialStatusHistory([FromRoute] string userId)
    {
        var result = await _formService.GetFinancialStatusHistory(userId);
        return Ok(result);
    }
}
