using TradeStream.Application.DTOs.Response;
using TradeStream.Domain.Entities;

namespace TradeStream.Domain.Interfaces;

public interface IUserService
{
    Task<UserResponse> CreateUserAsync(CancellationToken cancellationToken);
    Task<UserResponse> GetUserAsync(Guid id, CancellationToken cancellationToken);
    Task<UserResponse> DepositUserBalanceAsync(Guid userId, decimal amount, CancellationToken cancellationToken);
    Task<UserResponse> WithdrawUserBalanceAsync(Guid userId, decimal amount, CancellationToken cancellationToken);
    Task DisableUserAsync(Guid userId, CancellationToken cancellationToken);
    Task EnableUserAsync(Guid userId, CancellationToken cancellationToken);
    Task<IEnumerable<PositionResponse>> GetUserPositionsAsync(Guid userId, int size, int page, CancellationToken cancellationToken);
}