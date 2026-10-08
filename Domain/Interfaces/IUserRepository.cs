using TradeStream.Domain.Entities;

namespace TradeStream.Domain.Interfaces;

public interface IUserRepository {
    Task<User> AddUserAsync(User user, CancellationToken cancellationToken);
    Task<User?> GetUserAsync(Guid id, CancellationToken cancellationToken);
    Task<User> UpdateUserBalanceAsync(User user, CancellationToken cancellationToken);
    Task UpdateUserActivityStatusAsync(User user, CancellationToken cancellationToken);
}