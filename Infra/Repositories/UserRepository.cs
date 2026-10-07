using TradeStream.Domain.Entities;
using TradeStream.Domain.Interfaces;
using TradeStream.Infra.Database;

namespace TradeStream.Infra.Repositories;

public class UserRepository(TradeDbContext context) : IUserRepository{
    private readonly TradeDbContext _context = context;
    
    public async Task<User> AddUserAsync(User user, CancellationToken cancellationToken)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync(cancellationToken);
        return user;
    }

    public async Task<User?> GetUserAsync(Guid id, CancellationToken cancellationToken)
    {
        User? user = await _context.Users.FindAsync(id, cancellationToken);
        return user;
    }
}