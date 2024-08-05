using FinanceApp.Server.Models.DTO;
using FinanceApp.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace FinanceApp.Server.Controllers;
[Route("[controller]")]
[ApiController]
public class FinancialFormController : ControllerBase
{
    private readonly FormService _formService;

    public FinancialFormController(FormService formService)
    {
        _formService = formService;
    }

    [HttpPost("")]
    public async Task<ActionResult<FinancialForm>> SubmitForm([FromBody] FinancialForm model)
    {
        var result = await _formService.SubmitForm(model);
        return Ok(result);
    }
}
