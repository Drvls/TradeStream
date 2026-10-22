using Microsoft.AspNetCore.Mvc;
using TradeStream.Application.DTOs.Response;
using TradeStream.Domain.Interfaces;

namespace TradeStream.Domain.Controllers;

[ApiController]
[Route("tradestream/[controller]")]
public class UserController(IUserService userService) : ControllerBase{
    private readonly IUserService _userService = userService;

    [HttpPost]
    public async Task<IActionResult> CreateUser(CancellationToken cancellationToken)
    {
        UserResponse response = await _userService.CreateUserAsync(cancellationToken);
        return Created($"/users/{response.Id}", response);
    }

    [HttpGet]
    public async Task<IActionResult> GetUser(Guid id, CancellationToken cancellationToken)
    {
        UserResponse response = await _userService.GetUserAsync(id, cancellationToken);
        return Ok(response);
    }

    [HttpPatch("Deposit")]
    public async Task<IActionResult> DepositUserBalance(Guid id, decimal amount, CancellationToken cancellationToken)
    {
        UserResponse response = await _userService.DepositUserBalanceAsync(id, amount, cancellationToken);
        return Ok(response);
    }
    
    [HttpPatch("Withdraw")]
    public async Task<IActionResult> WithdrawUserBalance(Guid id, decimal amount, CancellationToken cancellationToken)
    {
        UserResponse response = await _userService.WithdrawUserBalanceAsync(id, amount, cancellationToken);
        return Ok(response);
    }

    [HttpPatch("Disable")]
    public async Task<IActionResult> DisableUser(Guid id, CancellationToken cancellationToken)
    {
        await _userService.DisableUserAsync(id, cancellationToken);
        return NoContent();
    }
    
    [HttpPatch("Enable")]
    public async Task<IActionResult> EnableUser(Guid id, CancellationToken cancellationToken)
    {
        await _userService.EnableUserAsync(id, cancellationToken);
        return NoContent();
    }

    [HttpGet("Positions")]
    public async Task<IActionResult> GetUserPositionsAsync(
        Guid userId, int size, int page, CancellationToken cancellationToken)
    {
        IEnumerable<PositionResponse> responses =
            await _userService.GetUserPositionsAsync(userId, size, page, cancellationToken);
        return Ok(responses);
    }
}