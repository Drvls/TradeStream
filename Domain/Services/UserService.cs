using TradeStream.Application.DTOs.Response;
using TradeStream.Domain.Entities;
using TradeStream.Domain.Exceptions;
using TradeStream.Domain.Interfaces;

namespace TradeStream.Domain.Services;

public class UserService(IUserRepository userRepository) : IUserService{
    private readonly IUserRepository _userRepository = userRepository;
    
    public async Task<UserResponse> CreateUserAsync(CancellationToken cancellationToken)
    {
        User user = new User();
        return new UserResponse(await _userRepository.AddUserAsync(user, cancellationToken));
    }

    public async Task<UserResponse> GetUserAsync(Guid id, CancellationToken cancellationToken)
    {
        User user = await _userRepository.GetUserAsync(id, cancellationToken) ?? throw new UserNotFoundException(id);
        return new UserResponse(user);
    }
}