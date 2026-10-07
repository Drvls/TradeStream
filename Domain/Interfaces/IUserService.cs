using TradeStream.Application.DTOs.Response;

namespace TradeStream.Domain.Interfaces;

public interface IUserService
{
    Task<UserResponse> CreateUserAsync(CancellationToken cancellationToken);
}