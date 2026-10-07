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
        return Created($"/users/{response.User.Id}", response);
    }
}