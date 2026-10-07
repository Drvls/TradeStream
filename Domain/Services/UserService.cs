using TradeStream.Application.DTOs.Response;
using TradeStream.Domain.Entities;
using TradeStream.Domain.Interfaces;

namespace TradeStream.Domain.Services;

public class UserService(IUserRepository userRepository) : IUserService{
    private readonly IUserRepository _userRepository = userRepository;
    
    public async Task<UserResponse> CreateUserAsync(CancellationToken cancellationToken)
    {
        User user = new User();
        return new UserResponse(await _userRepository.AddUserAsync(user, cancellationToken));
    }
}