namespace TradeStream.Domain.Exceptions;

public class UserAlreadyDisableException(Guid id) : Exception($"User with id {id} is already disabled");