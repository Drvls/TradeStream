namespace TradeStream.Domain.Exceptions;

public class UserAlreadyEnableException(Guid id) : Exception($"User with id {id} is already enabled");