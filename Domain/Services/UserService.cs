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
        User newUser = await _userRepository.AddUserAsync(user, cancellationToken);
        UserResponse response = new UserResponse(newUser.Id,
            newUser.Balance,
            newUser.Positions,
            newUser.Orders,
            newUser.IsActive);
        return response;
    }

    public async Task<UserResponse> GetUserAsync(Guid id, CancellationToken cancellationToken)
    {
        User user = await _userRepository.GetUserAsync(id, cancellationToken) ?? throw new UserNotFoundException(id);
        UserResponse response = new UserResponse(user.Id,
            user.Balance,
            user.Positions,
            user.Orders,
            user.IsActive);
        return response;
    }

    public async Task<UserResponse> DepositUserBalanceAsync(Guid userId, decimal amount, CancellationToken cancellationToken)
    {
        User user = await _userRepository.GetUserAsync(userId, cancellationToken) ??
                    throw new UserNotFoundException(userId);
        if(!user.IsActive) throw new UserDisabledException(user.Id);
        
        user.Deposit(amount);
        User newUser = await _userRepository.UpdateUserBalanceAsync(user, cancellationToken);
        UserResponse response = new UserResponse(newUser.Id,
            newUser.Balance,
            newUser.Positions,
            newUser.Orders,
            newUser.IsActive);
        return response;
    }
    
    public async Task<UserResponse> WithdrawUserBalanceAsync(Guid userId, decimal amount, CancellationToken cancellationToken)
    {
        User user = await _userRepository.GetUserAsync(userId, cancellationToken) ??
                    throw new UserNotFoundException(userId);
        if(!user.IsActive) throw new UserDisabledException(user.Id);
        
        user.Withdraw(amount);
        User newUser = await _userRepository.UpdateUserBalanceAsync(user, cancellationToken);
        UserResponse response = new UserResponse(newUser.Id,
            newUser.Balance,
            newUser.Positions,
            newUser.Orders,
            newUser.IsActive);
        return response;
    }

    public async Task EnableUserAsync(Guid userId, CancellationToken cancellationToken)
    {
        User user = await _userRepository.GetUserAsync(userId, cancellationToken) ?? throw new UserNotFoundException(userId);
        if(user.IsActive) throw new UserAlreadyEnableException(user.Id);
        
        user.Enable();
        await _userRepository.UpdateUserActivityStatusAsync(user, cancellationToken);
    }

    public async Task DisableUserAsync(Guid userId, CancellationToken cancellationToken)
    {
        User user = await _userRepository.GetUserAsync(userId, cancellationToken) ?? throw new UserNotFoundException(userId);
        if(!user.IsActive) throw new UserAlreadyDisableException(user.Id);
        
        user.Disable();
        await _userRepository.UpdateUserActivityStatusAsync(user, cancellationToken);
    }
}