using FinanceApp.Server.Data;
using FinanceApp.Server.Models.Definitions;
using FinanceApp.Server.Models.DTO;
using FinanceApp.Server.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinanceApp.Server.Controllers;
[Route("[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly DataContext _ctx;

    public UserController(UserManager<User> userManager, SignInManager<User> signInManager, DataContext ctx, FormService formService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _ctx = ctx;
    }

    [HttpPost("register")]
    public async Task<ActionResult<Guid>> Register([FromBody] RegisterDto registerDto)
    {
        var user = new User
        {
            UserName = registerDto.Email,
            Email = registerDto.Email,
        };

        var result = await _userManager.CreateAsync(user, registerDto.Password);

        if (!result.Succeeded) return BadRequest(result.Errors);

        return Ok();
    }

    [HttpPost("get-user-id")]
    public async Task<ActionResult<Guid>> GetUserId([FromBody] LoginDto loginDto)
    {
        var user = _ctx.Users.FirstOrDefault(x => x.Email == loginDto.Email);

        if (user == null)
        {
            return NotFound("User not found");
        }

        var userIdString = await _userManager.GetUserIdAsync(user);

        if (Guid.TryParse(userIdString, out var userId))
        {
            return Ok(userId);
        }

        return BadRequest("Invalid user ID format");
    }

    [HttpPost("log-out")]
    public async Task<IActionResult> LogOff()
    {
        await _signInManager.SignOutAsync();
        return Ok();
    }
}
