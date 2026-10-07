namespace TradeStream.Domain.Exceptions;

public class UserNotFoundException(Guid id) : Exception($"User with id '{id}' not found");