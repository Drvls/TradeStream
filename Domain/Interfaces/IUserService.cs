using TradeStream.Application.DTOs.Response;

namespace TradeStream.Domain.Interfaces;

public interface IUserService
{
    Task<UserResponse> CreateUserAsync(CancellationToken cancellationToken);
    Task<UserResponse> GetUserAsync(Guid id, CancellationToken cancellationToken);
}